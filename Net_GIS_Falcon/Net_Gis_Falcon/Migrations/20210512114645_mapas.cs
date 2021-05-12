using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class mapas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coordenadas",
                table: "zona",
                type: "character varying(30000)",
                maxLength: 30000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "color_estado",
                table: "estado",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coordenadas",
                table: "zona");

            migrationBuilder.AlterColumn<string>(
                name: "color_estado",
                table: "estado",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7,
                oldNullable: true);
        }
    }
}
