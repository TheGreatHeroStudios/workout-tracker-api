namespace model
{
	public partial class Exercise
	{
		public Exercise()
		{
			ExerciseMuscles = new HashSet<ExerciseMuscle>();
		}

		public int ExerciseId { get; set; }
		public string ExerciseName { get; set; } = null!;
		public string? ExerciseDesc { get; set; }
		public byte[]? ExerciseImage { get; set; }

		public virtual ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
	}
}
