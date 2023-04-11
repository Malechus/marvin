using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace marvin.Models;

public partial class HouseholdContext : DbContext
{
    private readonly string ConnectionString;

    public HouseholdContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public HouseholdContext(DbContextOptions<HouseholdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyChore> DailyChores { get; set; }

    public virtual DbSet<WeeklyChore> WeeklyChores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<DailyChore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("daily_chore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.ChoreName)
                .HasMaxLength(50)
                .HasColumnName("chore_name");
            entity.Property(e => e.Friday)
                .HasMaxLength(25)
                .HasColumnName("friday");
            entity.Property(e => e.Monday)
                .HasMaxLength(25)
                .HasColumnName("monday");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.Saturday)
                .HasMaxLength(25)
                .HasColumnName("saturday");
            entity.Property(e => e.Sunday)
                .HasMaxLength(25)
                .HasColumnName("sunday");
            entity.Property(e => e.Thursday)
                .HasMaxLength(25)
                .HasColumnName("thursday");
            entity.Property(e => e.Tuesday)
                .HasMaxLength(25)
                .HasColumnName("tuesday");
            entity.Property(e => e.Wednesday)
                .HasMaxLength(25)
                .HasColumnName("wednesday");
        });

        modelBuilder.Entity<WeeklyChore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("weekly_chore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.ChoreDay)
                .HasMaxLength(25)
                .HasColumnName("chore_day");
            entity.Property(e => e.ChoreName)
                .HasMaxLength(50)
                .HasColumnName("chore_name");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.Responsibility)
                .HasMaxLength(25)
                .HasColumnName("responsibility");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
