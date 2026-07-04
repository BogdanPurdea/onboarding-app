using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Data;

public class OnboardingDbContext(DbContextOptions<OnboardingDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<OnboardingTask> OnboardingTasks { get; set; }
    public DbSet<TaskPrerequisite> TaskPrerequisites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Departments ──────────────────────────────────────────────────────
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id)
                .UseIdentityColumn(); // Serial / auto-increment in PostgreSQL

            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Unique constraint on Name
            entity.HasIndex(d => d.Name)
                .IsUnique();

            // Check constraint – only the 5 core departments are allowed
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Departments_Name",
                "\"Name\" IN ('Engineering', 'Sales', 'Marketing', 'HR', 'Finance')"));

            // Seed the 5 core departments
            entity.HasData(
                new Department { Id = 1, Name = "Engineering" },
                new Department { Id = 2, Name = "Sales" },
                new Department { Id = 3, Name = "Marketing" },
                new Department { Id = 4, Name = "HR" },
                new Department { Id = 5, Name = "Finance" }
            );
        });

        // ── OnboardingTasks ──────────────────────────────────────────────────
        modelBuilder.Entity<OnboardingTask>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .UseIdentityColumn();

            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(t => t.Description)
                .IsRequired();

            // Store the TimelinePhase enum as an integer
            entity.Property(t => t.TimelinePhase)
                .HasConversion<int>();

            // Index on DepartmentId for fast role-specific queries
            // e.g. SELECT * FROM "OnboardingTasks" WHERE "DepartmentId" = 1
            entity.HasIndex(t => t.DepartmentId)
                .HasDatabaseName("IX_OnboardingTasks_DepartmentId");

            entity.HasOne(t => t.Department)
                .WithMany(d => d.OnboardingTasks)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── TaskPrerequisites ────────────────────────────────────────────────
        modelBuilder.Entity<TaskPrerequisite>(entity =>
        {
            // Composite primary key
            entity.HasKey(tp => new { tp.PreDependentTaskId, tp.PostDependentTaskId });

            // PreDependentTaskId → the task that must be completed first
            entity.HasOne(tp => tp.PreDependentTask)
                .WithMany(t => t.Dependents)
                .HasForeignKey(tp => tp.PreDependentTaskId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict to avoid cyclical cascades

            // PostDependentTaskId → the task that is unlocked after
            entity.HasOne(tp => tp.PostDependentTask)
                .WithMany(t => t.Prerequisites)
                .HasForeignKey(tp => tp.PostDependentTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
