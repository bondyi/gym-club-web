using System;
using System.Collections.Generic;
using API.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Models;

public partial class GymClubContext : DbContext
{
    public GymClubContext()
    {
    }

    public GymClubContext(DbContextOptions<GymClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveMembership> ActiveMemberships { get; set; }

    public virtual DbSet<AttendenceMark> AttendenceMarks { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseWorkout> ExerciseWorkouts { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<MembershipInfo> MembershipInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProgress> UserProgresses { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutProgram> WorkoutPrograms { get; set; }

    public virtual DbSet<WorkoutProgramWorkout> WorkoutProgramWorkouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveMembership>(entity =>
        {
            entity.HasKey(e => e.ActiveMembershipId).HasName("pk_active_memberships");

            entity.ToTable("active_memberships");

            entity.HasIndex(e => e.ActiveMembershipId, "active_memberships_pk").IsUnique();

            entity.HasIndex(e => e.UserId, "has_fk");

            entity.Property(e => e.ActiveMembershipId).HasColumnName("active_membership_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VisitsLeft).HasColumnName("visits_left");

            entity.HasOne(d => d.User).WithMany(p => p.ActiveMemberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_active_m_has_users");
        });

        modelBuilder.Entity<AttendenceMark>(entity =>
        {
            entity.HasKey(e => e.AttendenceMarkId).HasName("pk_attendence_marks");

            entity.ToTable("attendence_marks");

            entity.HasIndex(e => e.AttendenceMarkId, "attendence_marks_pk").IsUnique();

            entity.HasIndex(e => e.UserId, "makes_fk");

            entity.Property(e => e.AttendenceMarkId).HasColumnName("attendence_mark_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AttendenceMarks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_attenden_makes_users");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("pk_exercises");

            entity.ToTable("exercises");

            entity.HasIndex(e => e.ExerciseId, "exercises_pk").IsUnique();

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(36)
                .HasColumnName("exercise_name");
        });

        modelBuilder.Entity<ExerciseWorkout>(entity =>
        {
            entity.HasKey(e => e.ExerciseWorkoutId).HasName("pk_exercise_workout");

            entity.ToTable("exercise_workout");

            entity.HasIndex(e => e.ExerciseId, "ew_exercise_fk");

            entity.HasIndex(e => e.WorkoutId, "ew_workout_fk");

            entity.HasIndex(e => e.ExerciseWorkoutId, "exercise_workout_pk").IsUnique();

            entity.Property(e => e.ExerciseWorkoutId).HasColumnName("exercise_workout_id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.SupersetMark)
                .HasMaxLength(16)
                .HasColumnName("superset_mark");
            entity.Property(e => e.WorkoutId).HasColumnName("workout_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseWorkouts)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("fk_exercise_ew_exerci_exercise");

            entity.HasOne(d => d.Workout).WithMany(p => p.ExerciseWorkouts)
                .HasForeignKey(d => d.WorkoutId)
                .HasConstraintName("fk_exercise_ew_workou_workouts");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("pk_lessons");

            entity.ToTable("lessons");

            entity.HasIndex(e => e.LessonId, "lessons_pk").IsUnique();

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.LessonName)
                .HasMaxLength(32)
                .HasColumnName("lesson_name");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasMany(d => d.Users).WithMany(p => p.Lessons)
                .UsingEntity<Dictionary<string, object>>(
                    "UserLesson",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_les_user_less_users"),
                    l => l.HasOne<Lesson>().WithMany()
                        .HasForeignKey("LessonId")
                        .HasConstraintName("fk_user_les_user_less_lessons"),
                    j =>
                    {
                        j.HasKey("LessonId", "UserId").HasName("pk_user_lesson");
                        j.ToTable("user_lesson");
                        j.HasIndex(new[] { "UserId" }, "user_lesson2_fk");
                        j.HasIndex(new[] { "LessonId" }, "user_lesson_fk");
                        j.HasIndex(new[] { "LessonId", "UserId" }, "user_lesson_pk").IsUnique();
                        j.IndexerProperty<int>("LessonId").HasColumnName("lesson_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<MembershipInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("membership_info");

            entity.HasIndex(e => e.ProductId, "product_membership_fk");

            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MembershipType)
                .HasMaxLength(32)
                .HasColumnName("membership_type");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.VisitCount).HasColumnName("visit_count");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_membersh_product_m_products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("pk_products");

            entity.ToTable("products");

            entity.HasIndex(e => e.ProductId, "products_pk").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(32)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductType)
                .HasMaxLength(16)
                .HasColumnName("product_type");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("pk_purchases");

            entity.ToTable("purchases");

            entity.HasIndex(e => e.UserId, "pays_fk");

            entity.HasIndex(e => e.PurchaseId, "purchases_pk").IsUnique();

            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.PurchaseTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("purchase_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_purchase_pays_users");
        });

        modelBuilder.Entity<PurchaseProduct>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("pk_purchase_product");

            entity.ToTable("purchase_product");

            entity.HasIndex(e => e.ProductId, "contains_fk");

            entity.HasIndex(e => e.PurchaseId, "contains_pk").IsUnique();

            entity.Property(e => e.PurchaseId)
                .ValueGeneratedNever()
                .HasColumnName("purchase_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_purchase_contains_products");

            entity.HasOne(d => d.Purchase).WithOne(p => p.PurchaseProduct)
                .HasForeignKey<PurchaseProduct>(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_purchase_contains2_purchase");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("pk_reviews");

            entity.ToTable("reviews");

            entity.HasIndex(e => e.UserId, "leaves_fk");

            entity.HasIndex(e => e.ReviewId, "reviews_pk").IsUnique();

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.RatingValue).HasColumnName("rating_value");
            entity.Property(e => e.ReviewType)
                .HasMaxLength(16)
                .HasColumnName("review_type");
            entity.Property(e => e.TargetId).HasColumnName("target_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_reviews_leaves_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_users");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserId, "users_pk").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HashPassword)
                .HasMaxLength(120)
                .HasColumnName("hash_password");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(13)
                .HasColumnName("phone_number");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(120)
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenCreatedAt).HasColumnName("refresh_token_created_at");
            entity.Property(e => e.Surname)
                .HasMaxLength(32)
                .HasColumnName("surname");
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .HasDefaultValueSql("'client'::character varying")
                .HasColumnName("user_role");
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.UserProgressId).HasName("pk_user_progresses");

            entity.ToTable("user_progresses");

            entity.HasIndex(e => e.ExerciseWorkoutId, "stores_fk");

            entity.HasIndex(e => e.UserId, "tracks_fk");

            entity.HasIndex(e => e.UserProgressId, "user_progresses_pk").IsUnique();

            entity.Property(e => e.UserProgressId).HasColumnName("user_progress_id");
            entity.Property(e => e.CompletionTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("completion_time");
            entity.Property(e => e.ExerciseWorkoutId).HasColumnName("exercise_workout_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ExerciseWorkout).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.ExerciseWorkoutId)
                .HasConstraintName("fk_user_pro_stores_exercise");

            entity.HasOne(d => d.User).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_pro_tracks_users");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.WorkoutId).HasName("pk_workouts");

            entity.ToTable("workouts");

            entity.HasIndex(e => e.WorkoutId, "workouts_pk").IsUnique();

            entity.Property(e => e.WorkoutId).HasColumnName("workout_id");
        });

        modelBuilder.Entity<WorkoutProgram>(entity =>
        {
            entity.HasKey(e => e.WorkoutProgramId).HasName("pk_workout_programs");

            entity.ToTable("workout_programs");

            entity.HasIndex(e => e.WorkoutProgramId, "workout_programs_pk").IsUnique();

            entity.Property(e => e.WorkoutProgramId).HasColumnName("workout_program_id");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.DurationInWeeks).HasColumnName("duration_in_weeks");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.TrainingMethod)
                .HasMaxLength(16)
                .HasDefaultValueSql("'standard'::character varying")
                .HasColumnName("training_method");
            entity.Property(e => e.WorkoutProgramName)
                .HasMaxLength(36)
                .HasColumnName("workout_program_name");
            entity.Property(e => e.WorkoutProgramType)
                .HasMaxLength(12)
                .HasColumnName("workout_program_type");
            entity.Property(e => e.WorkoutsPerWeek).HasColumnName("workouts_per_week");
        });

        modelBuilder.Entity<WorkoutProgramWorkout>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("workout_program_workout");

            entity.HasIndex(e => e.ExerciseWorkoutId, "ew_wpw_fk");

            entity.HasIndex(e => e.WorkoutProgramId, "wp_wpw_fk");

            entity.Property(e => e.ExerciseWorkoutId).HasColumnName("exercise_workout_id");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.WorkoutProgramId).HasColumnName("workout_program_id");

            entity.HasOne(d => d.ExerciseWorkout).WithMany()
                .HasForeignKey(d => d.ExerciseWorkoutId)
                .HasConstraintName("fk_workout__ew_wpw_exercise");

            entity.HasOne(d => d.WorkoutProgram).WithMany()
                .HasForeignKey(d => d.WorkoutProgramId)
                .HasConstraintName("fk_workout__wp_wpw_workout_");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
