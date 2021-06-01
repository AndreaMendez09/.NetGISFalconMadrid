using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Gis_Falcon.Migrations
{
    public partial class coordenadas_peticion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coordenadas",
                table: "peticion",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coordenadas",
                table: "peticion");
        }
    }
}
