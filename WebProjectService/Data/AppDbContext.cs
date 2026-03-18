using Microsoft.EntityFrameworkCore;
using WebProjectService.Entities;

namespace WebProjectService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<WorkoutProgram> WorkoutPrograms => Set<WorkoutProgram>();
    public DbSet<WorkoutProgramExercise> WorkoutProgramExercises => Set<WorkoutProgramExercise>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasOne(x => x.User)
            .WithOne(x => x.MemberProfile)
            .HasForeignKey<Member>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Trainer>()
            .HasOne(x => x.User)
            .WithOne(x => x.TrainerProfile)
            .HasForeignKey<Trainer>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutProgramExercise>()
            .HasKey(x => new { x.WorkoutProgramId, x.ExerciseId });

        modelBuilder.Entity<WorkoutProgramExercise>()
            .HasOne(x => x.WorkoutProgram)
            .WithMany(x => x.ProgramExercises)
            .HasForeignKey(x => x.WorkoutProgramId);

        modelBuilder.Entity<WorkoutProgramExercise>()
            .HasOne(x => x.Exercise)
            .WithMany(x => x.WorkoutPrograms)
            .HasForeignKey(x => x.ExerciseId);

        modelBuilder.Entity<WorkoutSession>()
            .HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutSession>()
            .HasOne(x => x.Trainer)
            .WithMany()
            .HasForeignKey(x => x.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<WorkoutSession>()
            .HasOne(x => x.WorkoutProgram)
            .WithMany()
            .HasForeignKey(x => x.WorkoutProgramId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Measurement>()
            .Property(x => x.Weight)
            .HasPrecision(8, 2);

        modelBuilder.Entity<Measurement>()
            .Property(x => x.Height)
            .HasPrecision(8, 2);

        modelBuilder.Entity<Measurement>()
            .Property(x => x.BodyFatPercentage)
            .HasPrecision(5, 2);

        modelBuilder.Entity<MembershipPlan>()
            .Property(x => x.MonthlyPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Trainer>()
            .Property(x => x.SalaryAmount)
            .HasPrecision(10, 2);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.UpdatedDate = null;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
