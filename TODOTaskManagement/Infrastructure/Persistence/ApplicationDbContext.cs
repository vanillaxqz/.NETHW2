﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Domain.Entities.TaskState>();
            modelBuilder.HasPostgresEnum<TaskPriority>();
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.CreatedAt).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.DueDate);
                entity.Property(e => e.UpdatedAt).ValueGeneratedOnUpdate();
                entity.Property(e => e.State).IsRequired();
                entity.Property(e => e.Priority).IsRequired();
            });
        }
    }
}
