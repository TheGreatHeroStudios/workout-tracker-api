using System;
using System.Collections.Generic;

namespace model
{
	public partial class Exercise
	{
		public Exercise()
		{
			ExerciseMuscles = new HashSet<ExerciseMuscle>();
			WorkoutExercises = new HashSet<WorkoutExercise>();
		}

		public int ExerciseId { get; set; }
		public string ExerciseName { get; set; } = null!;
		public string? ExerciseDesc { get; set; }
		public byte[]? ExerciseImage { get; set; }

		public virtual ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
		public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
