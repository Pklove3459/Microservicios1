using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MSCompras.Models
{
    public partial class comprasContext : DbContext
    {
        public comprasContext()
        {
        }

        public comprasContext(DbContextOptions<comprasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<PlanesPago> PlanesPagos { get; set; }
        public virtual DbSet<ProductosCompra> ProductosCompras { get; set; }
        public virtual DbSet<TipoPlanPago> TipoPlanPagos { get; set; }

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
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compras");

                entity.HasIndex(e => e.PlanPagos, "fk_compras_plan_pagos_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descuento).HasColumnName("descuento");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.PlanPagos).HasColumnName("plan_pagos");

                entity.HasOne(d => d.PlanPagosNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.PlanPagos)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_compras_plan_pagos");
            });

            modelBuilder.Entity<PlanesPago>(entity =>
            {
                entity.ToTable("planes_pago");

                entity.HasIndex(e => e.Tipo, "fk_planes_tipo_planes_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mensualidades).HasColumnName("mensualidades");

                entity.Property(e => e.Notas)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("notas")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.PlanesPagos)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_planes_tipo_planes");
            });

            modelBuilder.Entity<ProductosCompra>(entity =>
            {
                entity.ToTable("productos_compra");

                entity.HasIndex(e => e.IdCompra, "fk_id_compras_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Costo).HasColumnName("costo");

                entity.Property(e => e.IdCompra).HasColumnName("idCompra");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Notas)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("notas")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.ProductosCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("fk_id_compras");
            });

            modelBuilder.Entity<TipoPlanPago>(entity =>
            {
                entity.ToTable("tipo_plan_pago");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abreviacion)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasColumnName("abreviacion")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

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
