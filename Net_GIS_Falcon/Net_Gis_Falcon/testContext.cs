using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Net_Gis_Falcon
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<HistoricoEstado> HistoricoEstados { get; set; }
        public virtual DbSet<Nivel> Nivels { get; set; }
        public virtual DbSet<OperadoresZona> OperadoresZonas { get; set; }
        public virtual DbSet<Peticion> Peticions { get; set; }
        public virtual DbSet<PeticionOperadore> PeticionOperadores { get; set; }
        public virtual DbSet<Respuestum> Respuesta { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Zona> Zonas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=ADMIN");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis")
                .HasAnnotation("Relational:Collation", "Spanish_Spain.1252");

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("direccion_pkey");

                entity.ToTable("direccion");

                entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("ciudad");

                entity.Property(e => e.CodPostal)
                    .HasMaxLength(15)
                    .HasColumnName("cod_postal");

                entity.Property(e => e.Escalera)
                    .HasMaxLength(25)
                    .HasColumnName("escalera");

                entity.Property(e => e.NombreVia)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("nombre_via");

                entity.Property(e => e.Notas)
                    .HasMaxLength(255)
                    .HasColumnName("notas");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("numero");

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("pais");

                entity.Property(e => e.Piso)
                    .HasMaxLength(25)
                    .HasColumnName("piso");

                entity.Property(e => e.TipoVia)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_via");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("estado_pkey");

                entity.ToTable("estado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.ColorEstado)
                    .HasMaxLength(7)
                    .HasColumnName("color_estado");

                entity.Property(e => e.Esfinal).HasColumnName("esfinal");

                entity.Property(e => e.NombreEstado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre_estado");

                entity.Property(e => e.Padre).HasColumnName("padre");

                entity.HasOne(d => d.PadreNavigation)
                    .WithMany(p => p.InversePadreNavigation)
                    .HasForeignKey(d => d.Padre)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("estado_padre_fkey");
            });

            modelBuilder.Entity<HistoricoEstado>(entity =>
            {
                entity.HasKey(e => new { e.Operador, e.Peticion, e.Estado })
                    .HasName("historico_estado_pkey");

                entity.ToTable("historico_estado");

                entity.Property(e => e.Operador).HasColumnName("operador");

                entity.Property(e => e.Peticion).HasColumnName("peticion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_modificacion");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.HistoricoEstados)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historico_estado_estado_fkey");

                entity.HasOne(d => d.OperadorNavigation)
                    .WithMany(p => p.HistoricoEstados)
                    .HasForeignKey(d => d.Operador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historico_estado_operador_fkey");

                entity.HasOne(d => d.PeticionNavigation)
                    .WithMany(p => p.HistoricoEstados)
                    .HasForeignKey(d => d.Peticion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historico_estado_peticion_fkey");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("nivel_pkey");

                entity.ToTable("nivel");

                entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");

                entity.Property(e => e.DescripcionPregunta)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion_pregunta");

                entity.Property(e => e.Nivel1).HasColumnName("nivel");
            });

            modelBuilder.Entity<OperadoresZona>(entity =>
            {
                entity.HasKey(e => new { e.Operador, e.Zona })
                    .HasName("operadores_zona_pkey");

                entity.ToTable("operadores_zona");

                entity.Property(e => e.Operador).HasColumnName("operador");

                entity.Property(e => e.Zona).HasColumnName("zona");

                entity.HasOne(d => d.OperadorNavigation)
                    .WithMany(p => p.OperadoresZonas)
                    .HasForeignKey(d => d.Operador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operadores_zona_operador_fkey");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.OperadoresZonas)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("operadores_zona_zona_fkey");
            });

            modelBuilder.Entity<Peticion>(entity =>
            {
                entity.HasKey(e => e.IdPeticion)
                    .HasName("peticion_pkey");

                entity.ToTable("peticion");

                entity.HasIndex(e => e.LocalizacionPeticion, "idx_code_peticion_geom")
                    .HasMethod("gist");

                entity.Property(e => e.IdPeticion).HasColumnName("id_peticion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.LocalizacionPeticion).HasColumnName("localizacion_peticion");

                entity.Property(e => e.PrecisionPeticion).HasColumnName("precision_peticion");

                entity.Property(e => e.Usuario).HasColumnName("usuario");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Peticions)
                    .HasForeignKey(d => d.Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("peticion_usuario_fkey");
            });

            modelBuilder.Entity<PeticionOperadore>(entity =>
            {
                entity.HasKey(e => new { e.Operador, e.Peticion })
                    .HasName("peticion_operadores_pkey");

                entity.ToTable("peticion_operadores");

                entity.Property(e => e.Operador).HasColumnName("operador");

                entity.Property(e => e.Peticion).HasColumnName("peticion");

                entity.HasOne(d => d.OperadorNavigation)
                    .WithMany(p => p.PeticionOperadores)
                    .HasForeignKey(d => d.Operador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("peticion_operadores_operador_fkey");

                entity.HasOne(d => d.PeticionNavigation)
                    .WithMany(p => p.PeticionOperadores)
                    .HasForeignKey(d => d.Peticion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("peticion_operadores_peticion_fkey");
            });

            modelBuilder.Entity<Respuestum>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("respuesta_pkey");

                entity.ToTable("respuesta");

                entity.Property(e => e.IdRespuesta).HasColumnName("id_respuesta");

                entity.Property(e => e.CuerpoRespuesta)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("cuerpo_respuesta");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.Principal).HasColumnName("principal");

                entity.Property(e => e.RespuestaPadre).HasColumnName("respuesta_padre");

                entity.HasOne(d => d.NivelNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.Nivel)
                    .HasConstraintName("respuesta_nivel_fkey");

                entity.HasOne(d => d.RespuestaPadreNavigation)
                    .WithMany(p => p.InverseRespuestaPadreNavigation)
                    .HasForeignKey(d => d.RespuestaPadre)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("respuesta_respuesta_padre_fkey");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("usuarios_pkey");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Email, "idx_usuarios_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "usuarios_email_key")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Foto)
                    .HasMaxLength(20)
                    .HasColumnName("foto");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("genero")
                    .IsFixedLength(true);

                entity.Property(e => e.Idioma)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("idioma");

                entity.Property(e => e.Municipio)
                    .HasMaxLength(20)
                    .HasColumnName("municipio");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rol).HasColumnName("rol");
            });

            
            modelBuilder.Entity<Zona>(entity =>
            {
                entity.HasKey(e => e.IdZona)
                    .HasName("zona_pkey");

                entity.ToTable("zona");

                entity.HasIndex(e => e.GeometriaZona, "idx_code_zona_geom")
                    .HasMethod("gist");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.Coordenadas)
                    .HasMaxLength(30000)
                    .HasColumnName("coordenadas");

                entity.Property(e => e.DescripcionZona)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion_zona");

                entity.Property(e => e.GeometriaZona).HasColumnName("geometria_zona");

                entity.Property(e => e.NombreZona)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre_zona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
