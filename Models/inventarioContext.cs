using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MSInventario.Models
{
    public partial class inventarioContext : DbContext
    {
        public inventarioContext()
        {
        }

        public inventarioContext(DbContextOptions<inventarioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Dispositivo> Dispositivos { get; set; }
        public virtual DbSet<Fabricante> Fabricantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.23-mysql"),
                    builder => {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.ToTable("dispositivos");

                entity.HasIndex(e => e.Categoria, "fk_dispositivos_categorias_idx");

                entity.HasIndex(e => e.Fabricante, "fk_dispositivos_fabricante_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoria).HasColumnName("categoria");

                entity.Property(e => e.Fabricante).HasColumnName("fabricante");

                entity.Property(e => e.Lanzamiento)
                    .HasColumnType("datetime")
                    .HasColumnName("lanzamiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.Dispositivos)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("fk_dispositivos_categorias");

                entity.HasOne(d => d.FabricanteNavigation)
                    .WithMany(p => p.Dispositivos)
                    .HasForeignKey(d => d.Fabricante)
                    .HasConstraintName("fk_dispositivos_fabricante");
            });

            modelBuilder.Entity<Fabricante>(entity =>
            {
                entity.ToTable("fabricante");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("nombre")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
