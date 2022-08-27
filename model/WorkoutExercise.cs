using System;
using System.Collections.Generic;

namespace model
{
	public partial class WorkoutExercise
	{
		public WorkoutExercise()
		{
			SetLaps = new HashSet<SetLap>();
		}

		public int WorkoutExerciseId { get; set; }
		public int WorkoutId { get; set; }
		public int ExerciseId { get; set; }

		public virtual Exercise Exercise { get; set; } = null!;
		public virtual Workout Workout { get; set; } = null!;
		public virtual ICollection<SetLap> SetLaps { get; set; }
	}
}
