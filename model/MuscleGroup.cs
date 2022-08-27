using System;
using System.Collections.Generic;

namespace model
{
	public partial class MuscleGroup
	{
		public MuscleGroup()
		{
			Muscles = new HashSet<Muscle>();
			Workouts = new HashSet<Workout>();
		}

		public int MuscleGroupId { get; set; }
		public string MuscleGroupDesc { get; set; } = null!;

		public virtual ICollection<Muscle> Muscles { get; set; }
		public virtual ICollection<Workout> Workouts { get; set; }
	}
}
