using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using model;

namespace persistence.Context
{
    public partial class WorkoutTrackerContext : DbContext
    {
		#region Constructor(s)
		public WorkoutTrackerContext()
        {
        }


        public WorkoutTrackerContext(DbContextOptions<WorkoutTrackerContext> options)
            : base(options)
        {
        }
		#endregion



		#region DbSet(s)
		public virtual DbSet<Muscle> Muscles { get; set; } = null!;
        public virtual DbSet<MuscleGroup> MuscleGroups { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<SetLap> SetLaps { get; set; } = null!;
        public virtual DbSet<WeighIn> WeighIns { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;
        public virtual DbSet<ExerciseMuscle> ExerciseMuscles { get; set; } = null!;
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;
		#endregion



		#region Override(s)
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Muscle>(entity =>
            {
                entity.HasKey(e => e.MuscleId)
                    .HasName("muscle_id");

                entity.ToTable("c_muscle");

                entity.Property(e => e.MuscleId).HasColumnName("muscle_id");

                entity.Property(e => e.MuscleGroupId).HasColumnName("muscle_group_id");

                entity.Property(e => e.MuscleLongDesc)
                    .HasMaxLength(50)
                    .HasColumnName("muscle_long_desc");

                entity.Property(e => e.MuscleShortDesc)
                    .HasMaxLength(20)
                    .HasColumnName("muscle_short_desc");

                entity.HasOne(d => d.MuscleGroup)
                    .WithMany(p => p.Muscles)
                    .HasForeignKey(d => d.MuscleGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cmuscle_cmusclegroup_musclegroupid");
            });

            modelBuilder.Entity<MuscleGroup>(entity =>
            {
                entity.HasKey(e => e.MuscleGroupId)
                    .HasName("muscle_group_id");

                entity.ToTable("c_muscle_group");

                entity.Property(e => e.MuscleGroupId).HasColumnName("muscle_group_id");

                entity.Property(e => e.MuscleGroupDesc)
                    .HasMaxLength(20)
                    .HasColumnName("muscle_group_desc");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("exercise");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.ExerciseDesc).HasColumnName("exercise_desc");

                entity.Property(e => e.ExerciseImage).HasColumnName("exercise_image");

                entity.Property(e => e.ExerciseName)
                    .HasMaxLength(50)
                    .HasColumnName("exercise_name");
            });

            modelBuilder.Entity<SetLap>(entity =>
            {
                entity.ToTable("set_lap");

                entity.Property(e => e.SetLapId).HasColumnName("set_lap_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.WeightLbs)
                    .HasColumnType("numeric(4, 1)")
                    .HasColumnName("weight_lbs");

                entity.Property(e => e.WorkoutExerciseId).HasColumnName("workout_exercise_id");

                entity.HasOne(d => d.WorkoutExercise)
                    .WithMany(p => p.SetLaps)
                    .HasForeignKey(d => d.WorkoutExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_setlap_xworkoutexercise_workoutexerciseid");
            });

            modelBuilder.Entity<WeighIn>(entity =>
            {
                entity.ToTable("weigh_in");

                entity.Property(e => e.WeighInId).HasColumnName("weigh_in_id");

                entity.Property(e => e.WeighInDate)
                    .HasColumnType("datetime")
                    .HasColumnName("weigh_in_date");

                entity.Property(e => e.WeightLbs)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("weight_lbs");
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("workout");

                entity.Property(e => e.WorkoutId).HasColumnName("workout_id");

                entity.Property(e => e.PrimaryMuscleGroupId).HasColumnName("primary_muscle_group_id");

                entity.Property(e => e.WorkoutDate)
                    .HasColumnType("datetime")
                    .HasColumnName("workout_date");

                entity.HasOne(d => d.PrimaryMuscleGroup)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.PrimaryMuscleGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_workout_cmusclegroup_musclegroupid");
            });

            modelBuilder.Entity<ExerciseMuscle>(entity =>
            {
                entity.HasKey(e => e.ExerciseMuscleId)
                    .HasName("exercise_muscle_id");

                entity.ToTable("x_exercise_muscle");

                entity.Property(e => e.ExerciseMuscleId).HasColumnName("exercise_muscle_id");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.MuscleId).HasColumnName("muscle_id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.ExerciseMuscles)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xexercisemuscle_exercise_exerciseid");

                entity.HasOne(d => d.Muscle)
                    .WithMany(p => p.ExerciseMuscles)
                    .HasForeignKey(d => d.MuscleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xexercisemuscle_cmuscle_muscleid");
            });

            modelBuilder.Entity<WorkoutExercise>(entity =>
            {
                entity.HasKey(e => e.WorkoutExerciseId)
                    .HasName("workout_exercise_id");

                entity.ToTable("x_workout_exercise");

                entity.Property(e => e.WorkoutExerciseId).HasColumnName("workout_exercise_id");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.WorkoutId).HasColumnName("workout_id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xworkoutexercise_exercise_exerciseid");

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.WorkoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xworkoutexercise_workout_workoutid");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
		#endregion
	}
}
