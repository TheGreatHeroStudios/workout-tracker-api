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
		public List<ExerciseDto> RetrieveExercises(int pageIndex, int count);
		public byte[]? RetrieveExerciseImageBytes(int exerciseId);
	}
}
