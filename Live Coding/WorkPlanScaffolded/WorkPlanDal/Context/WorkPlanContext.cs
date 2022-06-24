using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WorkPlanDal.Entities;
using Task = WorkPlanDal.Entities.Task;

namespace WorkPlanDal.Context
{
    public partial class WorkPlanContext : DbContext
    {
        public WorkPlanContext()
        {
        }

        public WorkPlanContext(DbContextOptions<WorkPlanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("server=localhost;port=5432;database=WorkPlanDb;user id=demo;password=Geheim123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mitarbeiter>(entity =>
            {
                entity.ToTable("Mitarbeiter");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasMany(d => d.Workers)
                    .WithMany(p => p.Tasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "PersonTask",
                        l => l.HasOne<Mitarbeiter>().WithMany().HasForeignKey("WorkersId"),
                        r => r.HasOne<Task>().WithMany().HasForeignKey("TasksId"),
                        j =>
                        {
                            j.HasKey("TasksId", "WorkersId");

                            j.ToTable("PersonTask");

                            j.HasIndex(new[] { "WorkersId" }, "IX_PersonTask_WorkersId");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
