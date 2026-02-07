using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace parcial1.Domain;

public partial class SupermercadoDbContext : DbContext
{
    public SupermercadoDbContext()
    {
    }

    public SupermercadoDbContext(DbContextOptions<SupermercadoDbContext> options)
    : base(options)
    {
    }

    public virtual DbSet<Consumidore> Consumidores { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consumidore>(entity =>
        {
            entity.HasKey(e => e.ConsumidorId).HasName("PK__Consumid__2C31D997E7F4248E");

            entity.HasIndex(e => e.Email, "UQ__Consumid__A9D10534BA3F97E0").IsUnique();

            entity.Property(e => e.ConsumidorId).HasColumnName("ConsumidorID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83501E6341");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Stock).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
