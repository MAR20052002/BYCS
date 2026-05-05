using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RRHH.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    id_inventario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.id_inventario);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id_item = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id_item);
                });

            migrationBuilder.CreateTable(
                name: "ItemStocks",
                columns: table => new
                {
                    id_item = table.Column<int>(type: "integer", nullable: false),
                    id_inventario = table.Column<int>(type: "integer", nullable: false),
                    codigo_item = table.Column<string>(type: "text", nullable: false),
                    codigo_inventario = table.Column<string>(type: "text", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStocks", x => new { x.id_item, x.id_inventario });
                    table.ForeignKey(
                        name: "FK_ItemStocks_Inventarios_id_inventario",
                        column: x => x.id_inventario,
                        principalTable: "Inventarios",
                        principalColumn: "id_inventario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemStocks_Items_id_item",
                        column: x => x.id_item,
                        principalTable: "Items",
                        principalColumn: "id_item",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_codigo",
                table: "Inventarios",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_codigo",
                table: "Items",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemStocks_codigo_item_codigo_inventario",
                table: "ItemStocks",
                columns: new[] { "codigo_item", "codigo_inventario" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemStocks_id_inventario",
                table: "ItemStocks",
                column: "id_inventario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemStocks");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
