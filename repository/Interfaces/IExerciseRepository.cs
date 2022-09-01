using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Interfaces
{
	public interface IExerciseRepository
	{
		Exercise? GetExerciseById(int exerciseId);
		byte[]? GetExerciseImageBytes(int exerciseId);
		List<Exercise> GetExercises(int pageIndex, int count);

		void CreateExercise(Exercise exercise, List<int> muscleIds);
		void UpdateExercise
		(
			Exercise trackedExercise, 
			Exercise updatedExerciseValues,
			List<int> updatedMuscleIds
		);
	}
}
