using dto;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Interfaces
{
	public interface IExerciseService : IWorkoutTrackerService<Exercise, ExerciseDto>
	{
		List<ExerciseDto> RetrieveExercises(int pageIndex, int count);
		byte[]? RetrieveExerciseImageBytes(int exerciseId);

		void CommitExercise
		(
			int exerciseId,
			string exerciseName,
			string? exerciseDescription,
			string? exerciseImageBase64,
			List<int> muscleIds
		);
	}
}
