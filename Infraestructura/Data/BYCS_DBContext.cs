using Microsoft.EntityFrameworkCore;
using BYCS.Core.Models;

namespace BYCS.Infraestructura.Data
{
    public class BYCS_DBContext : DbContext
    {
        public BYCS_DBContext(DbContextOptions<BYCS_DBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = default!;

        public DbSet<UsuarioTelefono> UsuarioTelefonos { get; set; } = default!;
        public DbSet<Telefono> Telefonos { get; set; } = default!;
        public DbSet<UsuarioCorreo> UsuarioCorreos { get; set; } = default!;
        public DbSet<Correo> Correos { get; set; } = default!;
        public DbSet<Solicitud> Solicitudes { get; set; } = default!;
        public DbSet<SolicitudProtocolo> SolicitudProtocolos { get; set; } = default!;
        public DbSet<Protocolo> Protocolos { get; set; } = default!;
        public DbSet<Respuesta> Respuestas { get; set; } = default!;
        public DbSet<RespuestaInforme> RespuestaInformes { get; set; } = default!;
        public DbSet<Informe> Informes { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemStock> ItemStocks { get; set; } = default!;
        public DbSet<Inventario> Inventarios { get; set; } = default!;

        //public DbSet<PersonaEmail> PersonaEmails { get; set; } = default!;


        /*public DbSet<EmpleadosActivosView> EmpleadosActivosViews { get; set; }
        public DbSet<HistorialDepartamentosView> HistorialDepartamentosViews { get; set; }
        public DbSet<ResumenNominaEmpleadoView> ResumenNominaEmpleadoViews { get; set; }
        public DbSet<ReportesEmpleadosView> ReportesEmpleadosViews { get; set; }
        public DbSet<EmpleadosSalariosPuestosView> EmpleadosSalariosPuestosViews { get; set; }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Recorre todas las entidades y propiedades DateTime
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Si la propiedad es DateTime o DateTime?
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("date"); // Se guarda como "date" en PostgreSQL
                    }
                }
            }
            // Claves compuestas
            /*modelBuilder.Entity<Empleado>()
            .HasOne(u => u.Persona)          // Un Usuario tiene un Perfil
            .WithOne(p => p.Empleado)        // Un Perfil pertenece a un Usuario
            .HasForeignKey<Empleado>(p => p.PersonaId);*/
            modelBuilder.Entity<UsuarioTelefono>().HasKey(pt => new { pt.id_usuario, pt.id_telefono, pt.fecha_inicio });
            modelBuilder.Entity<UsuarioCorreo>().HasKey(pe => new { pe.id_usuario, pe.id_correo, pe.fecha_inicio });
            modelBuilder.Entity<SolicitudProtocolo>().HasKey(pe => new { pe.id_protocolo, pe.id_solicitud });
            modelBuilder.Entity<Respuesta>().HasKey(pe => new { pe.id_solicitud, pe.id_usuario, pe.fecha });
            modelBuilder.Entity<RespuestaInforme>().HasKey(pe => new { pe.id_solicitud, pe.id_usuario, pe.id_informe });
            modelBuilder.Entity<ItemStock>().HasKey(pt => new { pt.id_item, pt.id_inventario });

            /*modelBuilder.Entity<EmpleadosActivosView>().HasNoKey();
            modelBuilder.Entity<HistorialDepartamentosView>().HasNoKey();
            modelBuilder.Entity<ResumenNominaEmpleadoView>().HasNoKey();
            modelBuilder.Entity<ReportesEmpleadosView>().HasNoKey();
            modelBuilder.Entity<EmpleadosSalariosPuestosView>().HasNoKey();*/
        }
    }
}
