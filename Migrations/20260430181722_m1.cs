using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RRHH.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Correos",
                columns: table => new
                {
                    id_correo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    correo = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correos", x => x.id_correo);
                });

            migrationBuilder.CreateTable(
                name: "Informes",
                columns: table => new
                {
                    id_informe = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_informe = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informes", x => x.id_informe);
                });

            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    id_protocolo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.id_protocolo);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    id_telefono = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    telf = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.id_telefono);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ci = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido_p = table.Column<string>(type: "text", nullable: true),
                    apellido_m = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    id_solicitud = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.id_solicitud);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCorreos",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_correo = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    correo = table.Column<string>(type: "text", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCorreos", x => new { x.id_usuario, x.id_correo, x.fecha_inicio });
                    table.ForeignKey(
                        name: "FK_UsuarioCorreos_Correos_id_correo",
                        column: x => x.id_correo,
                        principalTable: "Correos",
                        principalColumn: "id_correo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCorreos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTelefonos",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_telefono = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    telf = table.Column<string>(type: "text", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTelefonos", x => new { x.id_usuario, x.id_telefono, x.fecha_inicio });
                    table.ForeignKey(
                        name: "FK_UsuarioTelefonos_Telefonos_id_telefono",
                        column: x => x.id_telefono,
                        principalTable: "Telefonos",
                        principalColumn: "id_telefono",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTelefonos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    id_solicitud = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    codigo_solicitud = table.Column<string>(type: "text", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    aprobado = table.Column<bool>(type: "boolean", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => new { x.id_solicitud, x.id_usuario, x.fecha });
                    table.ForeignKey(
                        name: "FK_Respuestas_Solicitudes_id_solicitud",
                        column: x => x.id_solicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "id_solicitud",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Respuestas_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudProtocolos",
                columns: table => new
                {
                    id_protocolo = table.Column<int>(type: "integer", nullable: false),
                    id_solicitud = table.Column<int>(type: "integer", nullable: false),
                    codigo_protocolo = table.Column<string>(type: "text", nullable: false),
                    codigo_solicitud = table.Column<string>(type: "text", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudProtocolos", x => new { x.id_protocolo, x.id_solicitud });
                    table.ForeignKey(
                        name: "FK_SolicitudProtocolos_Protocolos_id_protocolo",
                        column: x => x.id_protocolo,
                        principalTable: "Protocolos",
                        principalColumn: "id_protocolo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudProtocolos_Solicitudes_id_solicitud",
                        column: x => x.id_solicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "id_solicitud",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaInformes",
                columns: table => new
                {
                    id_solicitud = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_informe = table.Column<int>(type: "integer", nullable: false),
                    codigo_solicitud = table.Column<string>(type: "text", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    codigo_informe = table.Column<string>(type: "text", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false),
                    Respuestafecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Respuestaid_solicitud = table.Column<int>(type: "integer", nullable: true),
                    Respuestaid_usuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaInformes", x => new { x.id_solicitud, x.id_usuario, x.id_informe });
                    table.ForeignKey(
                        name: "FK_RespuestaInformes_Informes_id_informe",
                        column: x => x.id_informe,
                        principalTable: "Informes",
                        principalColumn: "id_informe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespuestaInformes_Respuestas_Respuestaid_solicitud_Respuest~",
                        columns: x => new { x.Respuestaid_solicitud, x.Respuestaid_usuario, x.Respuestafecha },
                        principalTable: "Respuestas",
                        principalColumns: new[] { "id_solicitud", "id_usuario", "fecha" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Correos_correo",
                table: "Correos",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informes_codigo_informe",
                table: "Informes",
                column: "codigo_informe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_codigo",
                table: "Protocolos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaInformes_codigo_solicitud_ci_codigo_informe",
                table: "RespuestaInformes",
                columns: new[] { "codigo_solicitud", "ci", "codigo_informe" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaInformes_id_informe",
                table: "RespuestaInformes",
                column: "id_informe");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaInformes_Respuestaid_solicitud_Respuestaid_usuario~",
                table: "RespuestaInformes",
                columns: new[] { "Respuestaid_solicitud", "Respuestaid_usuario", "Respuestafecha" });

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_codigo_solicitud_ci",
                table: "Respuestas",
                columns: new[] { "codigo_solicitud", "ci" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_id_usuario",
                table: "Respuestas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_codigo",
                table: "Solicitudes",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_id_usuario",
                table: "Solicitudes",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudProtocolos_codigo_protocolo_codigo_solicitud_fecha~",
                table: "SolicitudProtocolos",
                columns: new[] { "codigo_protocolo", "codigo_solicitud", "fecha_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudProtocolos_id_solicitud",
                table: "SolicitudProtocolos",
                column: "id_solicitud");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_telf",
                table: "Telefonos",
                column: "telf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCorreos_ci_correo_fecha_inicio",
                table: "UsuarioCorreos",
                columns: new[] { "ci", "correo", "fecha_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCorreos_id_correo",
                table: "UsuarioCorreos",
                column: "id_correo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ci",
                table: "Usuarios",
                column: "ci",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTelefonos_ci_telf_fecha_inicio",
                table: "UsuarioTelefonos",
                columns: new[] { "ci", "telf", "fecha_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTelefonos_id_telefono",
                table: "UsuarioTelefonos",
                column: "id_telefono");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespuestaInformes");

            migrationBuilder.DropTable(
                name: "SolicitudProtocolos");

            migrationBuilder.DropTable(
                name: "UsuarioCorreos");

            migrationBuilder.DropTable(
                name: "UsuarioTelefonos");

            migrationBuilder.DropTable(
                name: "Informes");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Protocolos");

            migrationBuilder.DropTable(
                name: "Correos");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
