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

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Personasistema> Personasistemas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

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

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("personas_pkey");

                entity.ToTable("personas");

                entity.Property(e => e.IdPersona)
                    .ValueGeneratedNever()
                    .HasColumnName("id_persona");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("email");

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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Personasistema>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("personas_pkey");

                entity.ToTable("personasistema");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("email");

                entity.Property(e => e.Foto)
                    .HasMaxLength(20)
                    .HasColumnName("foto");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("genero")
                    .IsFixedLength(true);

                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

                entity.Property(e => e.Idioma)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("idioma");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rol).HasColumnName("rol");

                entity.Property(e => e.Zona)
                    .HasMaxLength(10)
                    .HasColumnName("zona");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("usuarios");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
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

                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
