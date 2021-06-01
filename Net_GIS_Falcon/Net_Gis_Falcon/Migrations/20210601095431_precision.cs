using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class precision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta");

            migrationBuilder.AddColumn<int>(
                name: "precision_peticion",
                table: "peticion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta",
                column: "respuesta_padre",
                principalTable: "respuesta",
                principalColumn: "id_respuesta",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta");

            migrationBuilder.DropColumn(
                name: "precision_peticion",
                table: "peticion");

            migrationBuilder.AddForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta",
                column: "respuesta_padre",
                principalTable: "respuesta",
                principalColumn: "id_respuesta",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
