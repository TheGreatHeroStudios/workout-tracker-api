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
	public class ExerciseRepository : WorkoutTrackerRepository<Exercise>, IExerciseRepository
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
				ComposeQueryable()
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
				ComposeQueryable()
					.OrderBy(exercise => exercise.ExerciseId)
					.Skip(pageIndex * count)
					.Take(count)
					.ToList();
		}


		public void CreateExercise(Exercise newExercise, List<int> muscleIds)
		{
			if(muscleIds.Any())
			{
				newExercise.ExerciseMuscles =
					new HashSet<ExerciseMuscle>
					(
						muscleIds
							.Select
							(
								muscleId =>
									new ExerciseMuscle
									{
										MuscleId = muscleId
									}
							)
					);
			}

			_context.Add(newExercise);
			_context.SaveChanges();
		}


		public void UpdateExercise
		(
			Exercise trackedExercise, 
			Exercise updatedExerciseValues,
			List<int> updatedMuscleIds
		)
		{
			trackedExercise.ExerciseName = updatedExerciseValues.ExerciseName;
			trackedExercise.ExerciseDesc = updatedExerciseValues.ExerciseDesc;
			trackedExercise.ExerciseImage = updatedExerciseValues.ExerciseImage;

			_context.Update(trackedExercise);

			/*trackedExercise.ExerciseMuscles = 
				trackedExercise
					.ExerciseMuscles
					.Join
					(
						//Keep previously tracked muscles that still
						//exist in the list of updated muscle ids.
						updatedMuscleIds,
						exMuscle => exMuscle.MuscleId,
						muscleId => muscleId,
						(exMuscle, muscleId) => exMuscle
					)
					.Concat
					(
						//Add in any newly added muscle ids that were 
						//not part of the previously tracked muscles.
						updatedMuscleIds
							.Except
							(
								trackedExercise
									.ExerciseMuscles
									.Select
									(
										exMuscle => exMuscle.MuscleId
									)
							)
							.Select
							(
								addedMuscleId =>
									new ExerciseMuscle
									{
										MuscleId = addedMuscleId
									}
							)
					)
					.ToList();*/

			//Added muscles include those which are included within the 
			//provided ids but were not previously on the tracked intity
			IEnumerable<ExerciseMuscle> addedMuscles =
				updatedMuscleIds
					.Except
					(
						trackedExercise
							.ExerciseMuscles
							.Select
							(
								exMuscle => exMuscle.MuscleId
							)
					)
					.Select
					(
						addedMuscleId =>
							new ExerciseMuscle
							{
								ExerciseId = trackedExercise.ExerciseId,
								MuscleId = addedMuscleId
							}
					);

			_context.AddRange(addedMuscles);

			//Removed muscle ids include those on the tracked entity
			//which were not included as part of the provided values
			IEnumerable<ExerciseMuscle> removedMuscles =
				trackedExercise
					.ExerciseMuscles
					.Where
					(
						exMuscle =>
							!updatedMuscleIds.Contains(exMuscle.MuscleId)
					);

			_context.RemoveRange(removedMuscles);

			_context.SaveChanges();
		}
		#endregion



		#region Override(s)
		protected override IQueryable<Exercise> ComposeQueryable() =>
			_context
				.Exercises
				.Include(exercise => exercise.ExerciseMuscles)
				.ThenInclude(exerciseMuscle => exerciseMuscle.Muscle)
				.ThenInclude(muscle => muscle.MuscleGroup);
		#endregion
	}
}
