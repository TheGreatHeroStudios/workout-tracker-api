using Microsoft.EntityFrameworkCore;
using model;
using persistence.Context;
using repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Implementation
{
	public class ExerciseRepository : WorkoutTrackerRepository, IExerciseRepository
	{
		#region Constructor(s)
		public ExerciseRepository(WorkoutTrackerContext context)
			: base(context)
		{
		}
		#endregion



		#region 'IExerciseRepository' Implementation
		public Exercise? GetExerciseById(int exerciseId)
		{
			return
				ComposeQueryable
					.SingleOrDefault
					(
						exercise =>
							exercise.ExerciseId == exerciseId
					);
		}


		public byte[]? GetExerciseImageBytes(int exerciseId)
		{
			return
				GetExerciseById(exerciseId)?.ExerciseImage;
		}


		public List<Exercise> GetExercises(int pageIndex, int count)
		{
			return
				ComposeQueryable
					.OrderBy(exercise => exercise.ExerciseId)
					.Skip(pageIndex * count)
					.Take(count)
					.ToList();
		}
		#endregion



		#region Non-Public Method(s)
		private IQueryable<Exercise> ComposeQueryable =>
			_context
				.Exercises
				.Include(exercise => exercise.ExerciseMuscles)
				.ThenInclude(exerciseMuscle => exerciseMuscle.Muscle)
				.ThenInclude(muscle => muscle.MuscleGroup);
		#endregion
	}
}
