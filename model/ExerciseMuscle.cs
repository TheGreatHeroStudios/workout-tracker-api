using System;
using System.Collections.Generic;

namespace model
{
	public partial class ExerciseMuscle
	{
		public int ExerciseMuscleId { get; set; }
		public int ExerciseId { get; set; }
		public int MuscleId { get; set; }

		public virtual Exercise Exercise { get; set; } = null!;
		public virtual Muscle Muscle { get; set; } = null!;
	}
}
