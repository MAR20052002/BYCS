using Microsoft.EntityFrameworkCore;
using BYCS.Core.Interfaces;
using BYCS.Infraestructura.Data;
using BYCS.Infraestructura.Repositorio;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Intentar obtener DATABASE_URL (Railway)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    // Convertir de formato postgres:// a formato Npgsql
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    connectionString = $"Host={uri.Host};" +
                       $"Port={uri.Port};" +
                       $"Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={userInfo[0]};" +
                       $"Password={userInfo[1]};" +
                       $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    // Usar configuración local (appsettings.json)
    connectionString = builder.Configuration.GetConnectionString("BYCSContext");
}

// Configurar DbContext
builder.Services.AddDbContext<BYCS_DBContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure();
    }));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

// Añadir controladores, Swagger y la API de endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar repositorios
builder.Services.AddScoped<ITelefonoRepositorio, TelefonoRepositorio>();
builder.Services.AddScoped<ICorreoRepositorio, CorreoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISolicitudRepositorio, SolicitudRepositorio>();
builder.Services.AddScoped<IProtocoloRepositorio, ProtocoloRepositorio>();
builder.Services.AddScoped<IUsuarioTelefonoRepositorio, UsuarioTelefonoRepositorio>();
builder.Services.AddScoped<IUsuarioCorreoRepositorio, UsuarioCorreoRepositorio>();
builder.Services.AddScoped<ISolicitudProtocoloRepositorio, SolicitudProtocoloRepositorio>();
builder.Services.AddScoped<IRespuestaRepositorio, RespuestaRepositorio>();
builder.Services.AddScoped<IRespuestaInformeRepositorio, RespuestaInformeRepositorio>();
builder.Services.AddScoped<IInformeRepositorio, InformeRepositorio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();
builder.Services.AddScoped<IItemStockRepositorio, ItemStockRepositorio>();
builder.Services.AddScoped<IItemRepositorio, ItemRepositorio>();
var app = builder.Build();

// Aplicar migraciones al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BYCS_DBContext>();
    try
    {
        dbContext.Database.Migrate(); // Aplica migraciones si no existen
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error aplicando migraciones: " + ex.Message);
    }

    // Ejecutar creación de vistas en la base de datos
    //await CrearVistas(dbContext);
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    //c.RoutePrefix = string.Empty; // Esto hace que Swagger esté en la raíz (puedes ajustarlo si necesitas otro lugar)
});

// Middleware
app.UseCors("MyApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Función para crear las vistas en la base de datos
/*async Task CrearVistas(BYCS_DBContext dbContext)
{
    try
    {
        var sql1 = @"
            CREATE OR REPLACE VIEW vw_EmpleadosActivos AS
            SELECT 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                p.""CI"", 
                p.""Nombre"", 
                p.""ApellidoPaterno"", 
                p.""ApellidoMaterno"", 
                p.""FechaNacimiento"", 
                p.""Sexo"", 
                e.""FechaIngreso""
            FROM 
                public.""Empleados"" e
            JOIN 
                public.""Personas"" p ON e.""PersonaId"" = p.""PersonaId""
            WHERE 
                e.""Estado"" = 'Activo';
        ";
        var sql2 = @"
            CREATE OR REPLACE VIEW vw_HistorialDepartamentos AS
            SELECT 
                h.""EmpleadoId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                d.""Codigo"" AS ""CodigoDepartamento"", 
                d.""Nombre"" AS ""NombreDepartamento"", 
                p.""Codigo"" AS ""CodigoPuesto"", 
                p.""Nombre"" AS ""NombrePuesto"", 
                h.""FechaInicio"", 
                h.""FechaFin"", 
                h.""Estado""
            FROM 
                public.""HistorialDepartamentos"" h
            JOIN 
                public.""Empleados"" e ON h.""EmpleadoId"" = e.""EmpleadoId""
            JOIN 
                public.""Departamentos"" d ON h.""DepartamentoId"" = d.""DepartamentoId""
            JOIN 
                public.""Puestos"" p ON h.""PuestoId"" = p.""PuestoId""
            WHERE 
                h.""Estado"" = 'Activo';
        ";
        var sql3 = @"
            CREATE OR REPLACE VIEW vw_ResumenNominaEmpleado AS
            SELECT 
                n.""NominaId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                e.""FechaIngreso"", 
                n.""PeriodoInicio"", 
                n.""PeriodoFin"", 
                n.""SalarioBase"", 
                n.""Bonos"", 
                n.""Descuentos"", 
                n.""TotalNeto"", 
                n.""Estado"" AS ""EstadoNomina""
            FROM 
                public.""Nominas"" n
            JOIN 
                public.""Empleados"" e ON n.""EmpleadoId"" = e.""EmpleadoId""
            WHERE 
                n.""Estado"" = 'Activo';
        ";
        var sql4 = @"
            CREATE OR REPLACE VIEW vw_ReportesEmpleados AS
            SELECT 
                r.""ReporteId"", 
                e.""Codigo"" AS ""CodigoEmpleadoReportado"", 
                d.""Codigo"" AS ""CodigoDepartamentoEmisor"", 
                r.""Fecha"", 
                r.""Tipo"", 
                r.""Descripcion"", 
                r.""Estado"" AS ""EstadoReporte""
            FROM 
                public.""ReportesEmpleados"" r
            JOIN 
                public.""Empleados"" e ON r.""EmpleadoReportadoId"" = e.""EmpleadoId""
            JOIN 
                public.""Departamentos"" d ON r.""DepartamentoEmisorId"" = d.""DepartamentoId""
            WHERE 
                r.""Estado"" = 'Activo';
        ";
        var sql5 = @"
            CREATE OR REPLACE VIEW vw_EmpleadosSalariosPuestos AS
            SELECT 
                e.""EmpleadoId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                p.""Nombre"" AS ""NombreEmpleado"", 
                p.""ApellidoPaterno"", 
                p.""ApellidoMaterno"", 
                s.""SalarioBase"", 
                pue.""Nombre"" AS ""NombrePuesto"", 
                e.""FechaIngreso"", 
                e.""Estado"" AS ""EstadoEmpleado""
            FROM 
                public.""Empleados"" e
            JOIN 
                public.""Personas"" p ON e.""PersonaId"" = p.""PersonaId""
            JOIN 
                public.""Nominas"" s ON e.""EmpleadoId"" = s.""EmpleadoId""
            JOIN 
                public.""HistorialDepartamentos"" h ON e.""EmpleadoId"" = h.""EmpleadoId""
            JOIN 
                public.""Puestos"" pue ON h.""PuestoId"" = pue.""PuestoId""
            WHERE 
                e.""Estado"" = 'Activo' 
                AND s.""Estado"" = 'Activo' 
                AND pue.""Estado"" = 'Activo';
        ";

        await dbContext.Database.ExecuteSqlRawAsync(sql1);
        await dbContext.Database.ExecuteSqlRawAsync(sql2);
        await dbContext.Database.ExecuteSqlRawAsync(sql3);
        await dbContext.Database.ExecuteSqlRawAsync(sql4);
        await dbContext.Database.ExecuteSqlRawAsync(sql5);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creando las vistas: {ex.Message}");
    }
}
*/