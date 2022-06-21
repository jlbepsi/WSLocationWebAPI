using System;
using System.Collections.Generic;
using System.Configuration;
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

        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Reglement> Reglements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["RHLocationDatabase"].ConnectionString;
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf32_general_ci")
                .HasCharSet("utf32");

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datedebut).HasColumnName("datedebut");

                entity.Property(e => e.Datefin).HasColumnName("datefin");

                entity.Property(e => e.IdHabitation).HasColumnName("id_habitation");

                entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");

                entity.Property(e => e.Montant)
                    .HasPrecision(10)
                    .HasColumnName("montant");
            });

            modelBuilder.Entity<Reglement>(entity =>
            {
                entity.ToTable("reglement");

                entity.HasIndex(e => e.IdLocation, "id_location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateversement).HasColumnName("dateversement");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.Montant)
                    .HasPrecision(10)
                    .HasColumnName("montant");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Reglements)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reglement_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
