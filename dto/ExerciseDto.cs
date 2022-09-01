namespace dto
{
	public class ExerciseDto
	{
		public int ExerciseId { get; set; }
		public string ExerciseName { get; set; } = string.Empty;
		public string? ExerciseDescription { get; set; }
		public string? ExerciseImageBase64 { get; set; }
		public List<MuscleDto>? Muscles { get; set; }
	}

	public class ExerciseUpdatePayload
	{
		public int? exerciseId {get; set;}
		public string exerciseName {get; set;} = string.Empty;
		public string? exerciseDescription {get; set;}
		public string? exerciseImageBase64 {get; set;}
		public List<int> muscleIds { get; set; } = new List<int>();
	}
}
