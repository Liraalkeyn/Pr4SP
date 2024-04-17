using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pr4SP.Models;

namespace Pr4SP.Context;

public partial class Pr4SpContext : DbContext
{
    public Pr4SpContext()
    {
    }

    public Pr4SpContext(DbContextOptions<Pr4SpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=Pr4SP;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("Transport_pkey");

            entity.ToTable("Transport");

            entity.Property(e => e.TransportId).HasColumnName("transportID");
            entity.Property(e => e.Color).HasMaxLength(25);
            entity.Property(e => e.Company).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
