using System;
using System.Collections.Generic;

namespace model
{
	public partial class SetLap
	{
		public int SetLapId { get; set; }
		public int WorkoutExerciseId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public decimal? WeightLbs { get; set; }

		public virtual WorkoutExercise WorkoutExercise { get; set; } = null!;
	}
}
