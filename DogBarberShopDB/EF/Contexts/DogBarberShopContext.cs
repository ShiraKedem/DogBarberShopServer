using System;
using System.Collections.Generic;
using DogBarberShopDB.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DogBarberShopDB.EF.Contexts;

public partial class DogBarberShopContext : DbContext
{
    public DogBarberShopContext()
    {
    }

    public DogBarberShopContext(DbContextOptions<DogBarberShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Queue> Queues { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QUEUES__3214EC07519A0A23");

            entity.ToTable("QUEUES");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Hour)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Queues)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__QUEUES__UserId__5FB337D6");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC0793A07209");

            entity.ToTable("USER");

            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
