using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dto
{
	public class ExerciseDto
	{
		public int ExerciseId { get; set; }
		public string? ExerciseName { get; set; }
		public string? ExerciseDescription { get; set; }
		public List<MuscleDto>? Muscles { get; set; }

		public static ExerciseDto FromModel(Exercise model)
		{
			return
				new ExerciseDto
				{
					ExerciseId = model.ExerciseId,
					ExerciseName = model.ExerciseName,
					ExerciseDescription = model.ExerciseDesc,
					Muscles =
						model
							.ExerciseMuscles
							.Select
							(
								em =>
									MuscleDto.FromModel(em.Muscle)
							)
							.ToList()
				};
		}
	}
}
