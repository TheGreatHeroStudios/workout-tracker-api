namespace model
{
	public partial class Muscle
	{
		public Muscle()
		{
			ExerciseMuscles = new HashSet<ExerciseMuscle>();
		}

		public int MuscleId { get; set; }
		public int MuscleGroupId { get; set; }
		public string MuscleShortDesc { get; set; } = null!;
		public string MuscleLongDesc { get; set; } = null!;

		public virtual MuscleGroup MuscleGroup { get; set; } = null!;
		public virtual ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
	}
}
