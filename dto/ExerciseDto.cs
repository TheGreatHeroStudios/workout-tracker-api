namespace dto
{
	public class ExerciseDto
	{
		public int ExerciseId { get; set; }
		public string? ExerciseName { get; set; }
		public string? ExerciseDescription { get; set; }
		public List<MuscleDto>? Muscles { get; set; }
	}
}
