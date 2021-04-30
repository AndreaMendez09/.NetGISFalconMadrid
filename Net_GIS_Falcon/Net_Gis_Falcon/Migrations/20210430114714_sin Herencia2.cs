using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Net_Gis_Falcon.Migrations
{
    public partial class sinHerencia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "direccion",
                columns: table => new
                {
                    id_direccion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_via = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nombre_via = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    ciudad = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    piso = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    escalera = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    numero = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    cod_postal = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    pais = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    notas = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("direccion_pkey", x => x.id_direccion);
                });

            migrationBuilder.CreateTable(
                name: "estado",
                columns: table => new
                {
                    id_estado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    esfinal = table.Column<bool>(type: "boolean", nullable: false),
                    color_estado = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("estado_pkey", x => x.id_estado);
                });

            migrationBuilder.CreateTable(
                name: "nivel",
                columns: table => new
                {
                    id_pregunta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion_pregunta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nivel = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("nivel_pkey", x => x.id_pregunta);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    apellido = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    genero = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false),
                    idioma = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    contraseña = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    foto = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    municipio = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    rol = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuarios_pkey", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "zona",
                columns: table => new
                {
                    id_zona = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_zona = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    descripcion_zona = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    geometria_zona = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("zona_pkey", x => x.id_zona);
                });

            migrationBuilder.CreateTable(
                name: "respuesta",
                columns: table => new
                {
                    id_respuesta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cuerpo_respuesta = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    principal = table.Column<bool>(type: "boolean", nullable: false),
                    nivel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("respuesta_pkey", x => x.id_respuesta);
                    table.ForeignKey(
                        name: "respuesta_nivel_fkey",
                        column: x => x.nivel,
                        principalTable: "nivel",
                        principalColumn: "id_pregunta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "peticion",
                columns: table => new
                {
                    id_peticion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha_creacion = table.Column<DateTime>(type: "date", nullable: false),
                    localizacion_peticion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("peticion_pkey", x => x.id_peticion);
                    table.ForeignKey(
                        name: "peticion_usuario_fkey",
                        column: x => x.usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "operadores_zona",
                columns: table => new
                {
                    operador = table.Column<int>(type: "integer", nullable: false),
                    zona = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("operadores_zona_pkey", x => new { x.operador, x.zona });
                    table.ForeignKey(
                        name: "operadores_zona_operador_fkey",
                        column: x => x.operador,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "operadores_zona_zona_fkey",
                        column: x => x.zona,
                        principalTable: "zona",
                        principalColumn: "id_zona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "historico_estado",
                columns: table => new
                {
                    estado = table.Column<int>(type: "integer", nullable: false),
                    peticion = table.Column<int>(type: "integer", nullable: false),
                    operador = table.Column<int>(type: "integer", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historico_estado_pkey", x => new { x.operador, x.peticion, x.estado });
                    table.ForeignKey(
                        name: "historico_estado_estado_fkey",
                        column: x => x.estado,
                        principalTable: "estado",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "historico_estado_operador_fkey",
                        column: x => x.operador,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "historico_estado_peticion_fkey",
                        column: x => x.peticion,
                        principalTable: "peticion",
                        principalColumn: "id_peticion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "peticion_operadores",
                columns: table => new
                {
                    operador = table.Column<int>(type: "integer", nullable: false),
                    peticion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("peticion_operadores_pkey", x => new { x.operador, x.peticion });
                    table.ForeignKey(
                        name: "peticion_operadores_operador_fkey",
                        column: x => x.operador,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "peticion_operadores_peticion_fkey",
                        column: x => x.peticion,
                        principalTable: "peticion",
                        principalColumn: "id_peticion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_historico_estado_estado",
                table: "historico_estado",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_historico_estado_peticion",
                table: "historico_estado",
                column: "peticion");

            migrationBuilder.CreateIndex(
                name: "IX_operadores_zona_zona",
                table: "operadores_zona",
                column: "zona");

            migrationBuilder.CreateIndex(
                name: "IX_peticion_usuario",
                table: "peticion",
                column: "usuario");

            migrationBuilder.CreateIndex(
                name: "IX_peticion_operadores_peticion",
                table: "peticion_operadores",
                column: "peticion");

            migrationBuilder.CreateIndex(
                name: "IX_respuesta_nivel",
                table: "respuesta",
                column: "nivel");

            migrationBuilder.CreateIndex(
                name: "usuarios_email_key",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "direccion");

            migrationBuilder.DropTable(
                name: "historico_estado");

            migrationBuilder.DropTable(
                name: "operadores_zona");

            migrationBuilder.DropTable(
                name: "peticion_operadores");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "estado");

            migrationBuilder.DropTable(
                name: "zona");

            migrationBuilder.DropTable(
                name: "peticion");

            migrationBuilder.DropTable(
                name: "nivel");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
