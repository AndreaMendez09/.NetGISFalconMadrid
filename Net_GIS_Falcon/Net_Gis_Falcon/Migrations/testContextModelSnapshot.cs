﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Net_Gis_Falcon;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

namespace Net_Gis_Falcon.Migrations
{
    [DbContext(typeof(testContext))]
    partial class testContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresExtension("postgis")
                .HasAnnotation("Relational:Collation", "Spanish_Spain.1252")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Net_Gis_Falcon.Direccion", b =>
                {
                    b.Property<int>("IdDireccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_direccion")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ciudad")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("ciudad");

                    b.Property<string>("CodPostal")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("cod_postal");

                    b.Property<string>("Escalera")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("escalera");

                    b.Property<string>("NombreVia")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)")
                        .HasColumnName("nombre_via");

                    b.Property<string>("Notas")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("notas");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("numero");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("pais");

                    b.Property<string>("Piso")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("piso");

                    b.Property<string>("TipoVia")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("tipo_via");

                    b.HasKey("IdDireccion")
                        .HasName("direccion_pkey");

                    b.ToTable("direccion");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Estado", b =>
                {
                    b.Property<int>("IdEstado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_estado")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ColorEstado")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("color_estado");

                    b.Property<bool>("Esfinal")
                        .HasColumnType("boolean")
                        .HasColumnName("esfinal");

                    b.Property<string>("NombreEstado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nombre_estado");

                    b.Property<int?>("Padre")
                        .HasColumnType("integer")
                        .HasColumnName("padre");

                    b.HasKey("IdEstado")
                        .HasName("estado_pkey");

                    b.HasIndex("Padre");

                    b.ToTable("estado");
                });

            modelBuilder.Entity("Net_Gis_Falcon.HistoricoEstado", b =>
                {
                    b.Property<int>("Operador")
                        .HasColumnType("integer")
                        .HasColumnName("operador");

                    b.Property<int>("Peticion")
                        .HasColumnType("integer")
                        .HasColumnName("peticion");

                    b.Property<int>("Estado")
                        .HasColumnType("integer")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("date")
                        .HasColumnName("fecha_modificacion");

                    b.HasKey("Operador", "Peticion", "Estado")
                        .HasName("historico_estado_pkey");

                    b.HasIndex("Estado");

                    b.HasIndex("Peticion");

                    b.ToTable("historico_estado");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Nivel", b =>
                {
                    b.Property<int>("IdPregunta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_pregunta")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DescripcionPregunta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("descripcion_pregunta");

                    b.Property<short>("Nivel1")
                        .HasColumnType("smallint")
                        .HasColumnName("nivel");

                    b.HasKey("IdPregunta")
                        .HasName("nivel_pkey");

                    b.ToTable("nivel");
                });

            modelBuilder.Entity("Net_Gis_Falcon.OperadoresZona", b =>
                {
                    b.Property<int>("Operador")
                        .HasColumnType("integer")
                        .HasColumnName("operador");

                    b.Property<int>("Zona")
                        .HasColumnType("integer")
                        .HasColumnName("zona");

                    b.HasKey("Operador", "Zona")
                        .HasName("operadores_zona_pkey");

                    b.HasIndex("Zona");

                    b.ToTable("operadores_zona");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Peticion", b =>
                {
                    b.Property<int>("IdPeticion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_peticion")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("date")
                        .HasColumnName("fecha_creacion");

                    b.Property<NpgsqlPoint>("LocalizacionPeticion")
                        .HasColumnType("point")
                        .HasColumnName("localizacion_peticion");

                    b.Property<int>("Usuario")
                        .HasColumnType("integer")
                        .HasColumnName("usuario");

                    b.HasKey("IdPeticion")
                        .HasName("peticion_pkey");

                    b.HasIndex("Usuario");

                    b.HasIndex(new[] { "LocalizacionPeticion" }, "idx_code_peticion_geom")
                        .HasMethod("gist");

                    b.ToTable("peticion");
                });

            modelBuilder.Entity("Net_Gis_Falcon.PeticionOperadore", b =>
                {
                    b.Property<int>("Operador")
                        .HasColumnType("integer")
                        .HasColumnName("operador");

                    b.Property<int>("Peticion")
                        .HasColumnType("integer")
                        .HasColumnName("peticion");

                    b.HasKey("Operador", "Peticion")
                        .HasName("peticion_operadores_pkey");

                    b.HasIndex("Peticion");

                    b.ToTable("peticion_operadores");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Respuestum", b =>
                {
                    b.Property<int>("IdRespuesta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_respuesta")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CuerpoRespuesta")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("cuerpo_respuesta");

                    b.Property<int>("Nivel")
                        .HasColumnType("integer")
                        .HasColumnName("nivel");

                    b.Property<bool>("Principal")
                        .HasColumnType("boolean")
                        .HasColumnName("principal");

                    b.Property<int?>("RespuestaPadre")
                        .HasColumnType("integer")
                        .HasColumnName("respuesta_padre");

                    b.HasKey("IdRespuesta")
                        .HasName("respuesta_pkey");

                    b.HasIndex("Nivel");

                    b.HasIndex("RespuestaPadre");

                    b.ToTable("respuesta");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("apellido");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("contraseña");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("date")
                        .HasColumnName("fecha_nacimiento");

                    b.Property<string>("Foto")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("foto");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character(5)")
                        .HasColumnName("genero")
                        .IsFixedLength(true);

                    b.Property<string>("Idioma")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("idioma");

                    b.Property<string>("Municipio")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("municipio");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nombre");

                    b.Property<int>("Rol")
                        .HasColumnType("integer")
                        .HasColumnName("rol");

                    b.HasKey("IdUsuario")
                        .HasName("usuarios_pkey");

                    b.HasIndex(new[] { "Email" }, "idx_usuarios_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Email" }, "usuarios_email_key")
                        .IsUnique();

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Zona", b =>
                {
                    b.Property<int>("IdZona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_zona")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Coordenadas")
                        .HasMaxLength(30000)
                        .HasColumnType("character varying(30000)")
                        .HasColumnName("coordenadas");

                    b.Property<string>("DescripcionZona")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("descripcion_zona");

                    b.Property<NpgsqlPolygon>("GeometriaZona")
                        .HasColumnType("polygon")
                        .HasColumnName("geometria_zona");

                    b.Property<string>("NombreZona")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nombre_zona");

                    b.HasKey("IdZona")
                        .HasName("zona_pkey");

                    b.HasIndex(new[] { "GeometriaZona" }, "idx_code_zona_geom")
                        .HasMethod("gist");

                    b.ToTable("zona");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Estado", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Estado", "PadreNavigation")
                        .WithMany("InversePadreNavigation")
                        .HasForeignKey("Padre")
                        .HasConstraintName("estado_padre_fkey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("PadreNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.HistoricoEstado", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Estado", "EstadoNavigation")
                        .WithMany("HistoricoEstados")
                        .HasForeignKey("Estado")
                        .HasConstraintName("historico_estado_estado_fkey")
                        .IsRequired();

                    b.HasOne("Net_Gis_Falcon.Usuario", "OperadorNavigation")
                        .WithMany("HistoricoEstados")
                        .HasForeignKey("Operador")
                        .HasConstraintName("historico_estado_operador_fkey")
                        .IsRequired();

                    b.HasOne("Net_Gis_Falcon.Peticion", "PeticionNavigation")
                        .WithMany("HistoricoEstados")
                        .HasForeignKey("Peticion")
                        .HasConstraintName("historico_estado_peticion_fkey")
                        .IsRequired();

                    b.Navigation("EstadoNavigation");

                    b.Navigation("OperadorNavigation");

                    b.Navigation("PeticionNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.OperadoresZona", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Usuario", "OperadorNavigation")
                        .WithMany("OperadoresZonas")
                        .HasForeignKey("Operador")
                        .HasConstraintName("operadores_zona_operador_fkey")
                        .IsRequired();

                    b.HasOne("Net_Gis_Falcon.Zona", "ZonaNavigation")
                        .WithMany("OperadoresZonas")
                        .HasForeignKey("Zona")
                        .HasConstraintName("operadores_zona_zona_fkey")
                        .IsRequired();

                    b.Navigation("OperadorNavigation");

                    b.Navigation("ZonaNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Peticion", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Usuario", "UsuarioNavigation")
                        .WithMany("Peticions")
                        .HasForeignKey("Usuario")
                        .HasConstraintName("peticion_usuario_fkey")
                        .IsRequired();

                    b.Navigation("UsuarioNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.PeticionOperadore", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Usuario", "OperadorNavigation")
                        .WithMany("PeticionOperadores")
                        .HasForeignKey("Operador")
                        .HasConstraintName("peticion_operadores_operador_fkey")
                        .IsRequired();

                    b.HasOne("Net_Gis_Falcon.Peticion", "PeticionNavigation")
                        .WithMany("PeticionOperadores")
                        .HasForeignKey("Peticion")
                        .HasConstraintName("peticion_operadores_peticion_fkey")
                        .IsRequired();

                    b.Navigation("OperadorNavigation");

                    b.Navigation("PeticionNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Respuestum", b =>
                {
                    b.HasOne("Net_Gis_Falcon.Nivel", "NivelNavigation")
                        .WithMany("Respuesta")
                        .HasForeignKey("Nivel")
                        .HasConstraintName("respuesta_nivel_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Net_Gis_Falcon.Respuestum", "RespuestaPadreNavigation")
                        .WithMany("InverseRespuestaPadreNavigation")
                        .HasForeignKey("RespuestaPadre")
                        .HasConstraintName("respuesta_respuesta_padre_fkey");

                    b.Navigation("NivelNavigation");

                    b.Navigation("RespuestaPadreNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Estado", b =>
                {
                    b.Navigation("HistoricoEstados");

                    b.Navigation("InversePadreNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Nivel", b =>
                {
                    b.Navigation("Respuesta");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Peticion", b =>
                {
                    b.Navigation("HistoricoEstados");

                    b.Navigation("PeticionOperadores");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Respuestum", b =>
                {
                    b.Navigation("InverseRespuestaPadreNavigation");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Usuario", b =>
                {
                    b.Navigation("HistoricoEstados");

                    b.Navigation("OperadoresZonas");

                    b.Navigation("PeticionOperadores");

                    b.Navigation("Peticions");
                });

            modelBuilder.Entity("Net_Gis_Falcon.Zona", b =>
                {
                    b.Navigation("OperadoresZonas");
                });
#pragma warning restore 612, 618
        }
    }
}
