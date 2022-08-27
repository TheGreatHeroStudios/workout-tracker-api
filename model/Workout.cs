using System;
using System.Collections.Generic;

namespace model
{
	public partial class Workout
	{
		public Workout()
		{
			WorkoutExercises = new HashSet<WorkoutExercise>();
		}

		public int WorkoutId { get; set; }
		public DateTime WorkoutDate { get; set; }
		public int PrimaryMuscleGroupId { get; set; }

		public virtual MuscleGroup PrimaryMuscleGroup { get; set; } = null!;
		public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
