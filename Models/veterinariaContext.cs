using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace veterinaria.Models
{
    public partial class veterinariaContext : DbContext
    {
        public veterinariaContext()
        {
        }

        public veterinariaContext(DbContextOptions<veterinariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita> Citas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<HistorialMedico> HistorialMedicos { get; set; } = null!;
        public virtual DbSet<Mascota> Mascotas { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9N4UR2R\\SQLEXPRESS;Initial Catalog=veterinaria; TrustServerCertificate=True;User ID=sa;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Citas__ClienteID__4E88ABD4");

                entity.HasOne(d => d.Mascota)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.MascotaId)
                    .HasConstraintName("FK__Citas__MascotaID__4F7CD00D");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorialMedico>(entity =>
            {
                entity.HasKey(e => e.HistorialId)
                    .HasName("PK__Historia__975206EFDFBAD468");

                entity.ToTable("HistorialMedico");

                entity.Property(e => e.HistorialId).HasColumnName("HistorialID");

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Diagnostico).HasColumnType("text");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Tratamiento).HasColumnType("text");

                entity.HasOne(d => d.Cita)
                    .WithMany(p => p.HistorialMedicos)
                    .HasForeignKey(d => d.CitaId)
                    .HasConstraintName("FK__Historial__CitaI__5441852A");
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.Property(e => e.MascotaId).HasColumnName("MascotaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Especie)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Raza)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Mascotas__Client__4BAC3F29");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.ServicioId).HasColumnName("ServicioID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
