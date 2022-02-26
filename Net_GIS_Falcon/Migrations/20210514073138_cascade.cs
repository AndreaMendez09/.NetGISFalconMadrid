using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "estado_padre_fkey",
                table: "estado");

            migrationBuilder.DropForeignKey(
                name: "respuesta_nivel_fkey",
                table: "respuesta");

            migrationBuilder.AddForeignKey(
                name: "estado_padre_fkey",
                table: "estado",
                column: "padre",
                principalTable: "estado",
                principalColumn: "id_estado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "respuesta_nivel_fkey",
                table: "respuesta",
                column: "nivel",
                principalTable: "nivel",
                principalColumn: "id_pregunta",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "estado_padre_fkey",
                table: "estado");

            migrationBuilder.DropForeignKey(
                name: "respuesta_nivel_fkey",
                table: "respuesta");

            migrationBuilder.AddForeignKey(
                name: "estado_padre_fkey",
                table: "estado",
                column: "padre",
                principalTable: "estado",
                principalColumn: "id_estado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "respuesta_nivel_fkey",
                table: "respuesta",
                column: "nivel",
                principalTable: "nivel",
                principalColumn: "id_pregunta",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
