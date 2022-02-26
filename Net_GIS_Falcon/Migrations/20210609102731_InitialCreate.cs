using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "operadores_zona_operador_fkey",
                table: "operadores_zona");

            migrationBuilder.DropForeignKey(
                name: "operadores_zona_zona_fkey",
                table: "operadores_zona");

            migrationBuilder.AddColumn<string>(
                name: "coordenadas",
                table: "peticion",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "operadores_zona_operador_fkey",
                table: "operadores_zona",
                column: "operador",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "operadores_zona_zona_fkey",
                table: "operadores_zona",
                column: "zona",
                principalTable: "zona",
                principalColumn: "id_zona",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "operadores_zona_operador_fkey",
                table: "operadores_zona");

            migrationBuilder.DropForeignKey(
                name: "operadores_zona_zona_fkey",
                table: "operadores_zona");

            migrationBuilder.DropColumn(
                name: "coordenadas",
                table: "peticion");

            migrationBuilder.AddForeignKey(
                name: "operadores_zona_operador_fkey",
                table: "operadores_zona",
                column: "operador",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "operadores_zona_zona_fkey",
                table: "operadores_zona",
                column: "zona",
                principalTable: "zona",
                principalColumn: "id_zona",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
