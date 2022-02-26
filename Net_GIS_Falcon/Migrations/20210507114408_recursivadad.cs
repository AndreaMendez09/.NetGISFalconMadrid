using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class recursivadad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descripcion_zona",
                table: "zona",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "rol",
                table: "usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "respuesta_padre",
                table: "respuesta",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "padre",
                table: "estado",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_respuesta_respuesta_padre",
                table: "respuesta",
                column: "respuesta_padre");

            migrationBuilder.AddForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta",
                column: "respuesta_padre",
                principalTable: "respuesta",
                principalColumn: "id_respuesta",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "respuesta_respuesta_padre_fkey",
                table: "respuesta");

            migrationBuilder.DropIndex(
                name: "IX_respuesta_respuesta_padre",
                table: "respuesta");

            migrationBuilder.DropColumn(
                name: "respuesta_padre",
                table: "respuesta");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion_zona",
                table: "zona",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "rol",
                table: "usuarios",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "padre",
                table: "estado",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
