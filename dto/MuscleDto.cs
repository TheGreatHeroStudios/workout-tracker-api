using model;

namespace dto
{
	public class MuscleDto
	{
		public int MuscleId { get; set; }
		public string? SimpleName { get; set; }
		public string? AnatomicalName { get; set; }
		public string? MuscleGroupName { get; set; }

		public static MuscleDto FromModel(Muscle model)
		{
			return
				new MuscleDto
				{
					MuscleId = model.MuscleId,
					AnatomicalName = model.MuscleLongDesc,
					SimpleName = model.MuscleShortDesc,
					MuscleGroupName = model.MuscleGroup.MuscleGroupDesc
				};
		}
	}
}