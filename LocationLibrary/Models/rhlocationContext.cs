using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LocationLibrary.Models
{
    public partial class rhlocationContext : DbContext
    {
        public rhlocationContext()
        {
        }

        public rhlocationContext(DbContextOptions<rhlocationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Facture> Factures { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Reglement> Reglements { get; set; } = null!;
        public virtual DbSet<Relance> Relances { get; set; } = null!;
        public virtual DbSet<Typereglement> Typereglements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.6.10;database=rhlocation;uid=adminrh;pwd=abcd4ABCD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf32_general_ci")
                .HasCharSet("utf32");

            modelBuilder.Entity<Facture>(entity =>
            {
                entity.ToTable("facture");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("Doit être identique à location_id");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(250)
                    .HasColumnName("adresse");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.FactureId, "facture_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datedebut)
                    .HasColumnType("datetime")
                    .HasColumnName("datedebut");

                entity.Property(e => e.Datefin)
                    .HasColumnType("datetime")
                    .HasColumnName("datefin");

                entity.Property(e => e.FactureId).HasColumnName("facture_id");

                entity.Property(e => e.Idhabitation).HasColumnName("idhabitation");

                entity.Property(e => e.Idutilisateur).HasColumnName("idutilisateur");

                entity.Property(e => e.Montanttotal)
                    .HasColumnName("montanttotal")
                    .HasDefaultValueSql("'0'");
                entity.Property(e => e.Montantverse)
                    .HasColumnName("montantverse")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Facture)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.FactureId)
                    .HasConstraintName("location_ibfk_1");
            });

            modelBuilder.Entity<Reglement>(entity =>
            {
                entity.ToTable("reglement");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.HasIndex(e => e.TypereglementId, "typereglement_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateversement)
                    .HasColumnType("datetime")
                    .HasColumnName("dateversement");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Montant)
                    .HasPrecision(10)
                    .HasColumnName("montant");

                entity.Property(e => e.TypereglementId).HasColumnName("typereglement_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Reglements)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reglement_ibfk_2");

                entity.HasOne(d => d.Typereglement)
                    .WithMany(p => p.Reglements)
                    .HasForeignKey(d => d.TypereglementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reglement_ibfk_1");
            });

            modelBuilder.Entity<Relance>(entity =>
            {
                entity.ToTable("relance");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Motif)
                    .HasMaxLength(250)
                    .HasColumnName("motif");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Relances)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("relance_ibfk_1");
            });

            modelBuilder.Entity<Typereglement>(entity =>
            {
                entity.ToTable("typereglement");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libelle)
                    .HasMaxLength(100)
                    .HasColumnName("libelle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
