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

        public virtual DbSet<Facture> Factures { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationOptionpayantero> LocationOptionpayanteros { get; set; }
        public virtual DbSet<Optionpayantero> Optionpayanteros { get; set; }
        public virtual DbSet<Reglement> Reglements { get; set; }
        public virtual DbSet<Relance> Relances { get; set; }
        public virtual DbSet<Typereglement> Typereglements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=rhlocation;uid=adminrh;pwd=abcd4ABCD", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Facture>(entity =>
            {
                entity.ToTable("facture");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("Doit être identique à location_id");

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("adresse");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.HasOne(d => d.IdLocation)
                    .WithOne(p => p.Facture)
                    .HasForeignKey<Facture>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("facture_ibfk_1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datedebut)
                    .HasColumnType("datetime")
                    .HasColumnName("datedebut");

                entity.Property(e => e.Datefin)
                    .HasColumnType("datetime")
                    .HasColumnName("datefin");

                entity.Property(e => e.Idhabitation).HasColumnName("idhabitation");

                entity.Property(e => e.Idutilisateur).HasColumnName("idutilisateur");

                entity.Property(e => e.Montanttotal).HasColumnName("montanttotal");

                entity.Property(e => e.Montantverse).HasColumnName("montantverse");
            });

            modelBuilder.Entity<LocationOptionpayantero>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.OptionpayanteroId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("location_optionpayantero");

                entity.HasIndex(e => e.OptionpayanteroId, "location_optionpayantero_id_fk");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.OptionpayanteroId).HasColumnName("optionpayantero_id");

                entity.Property(e => e.Prix).HasColumnName("prix");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationOptionpayanteros)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_optionpayantero_ibfk_1");

                entity.HasOne(d => d.Optionpayantero)
                    .WithMany(p => p.LocationOptionpayanteros)
                    .HasForeignKey(d => d.OptionpayanteroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("location_optionpayantero_id_fk");
            });

            modelBuilder.Entity<Optionpayantero>(entity =>
            {
                entity.ToTable("optionpayantero");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("libelle");
            });

            modelBuilder.Entity<Reglement>(entity =>
            {
                entity.ToTable("reglement");

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.HasIndex(e => e.TypereglementId, "typereglement_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateversement)
                    .HasColumnType("datetime")
                    .HasColumnName("dateversement")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Motif)
                    .IsRequired()
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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("libelle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
