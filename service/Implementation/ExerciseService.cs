using dto;
using model;
using repository.Interfaces;
using service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace service.Implementation
{
	public class ExerciseService : IExerciseService
	{
		#region Non-Public Member(s)
		private IExerciseRepository _repo;
		#endregion



		#region Constructor(s)
		public ExerciseService(IExerciseRepository repo)
		{
			_repo = repo;
		}
		#endregion



		#region 'IExerciseService' Implementation
		public List<ExerciseDto> RetrieveExercises(int pageIndex, int count)
		{
			return
				_repo
					.GetExercises(pageIndex, count)
					.Select
					(
						exercise => FromModel(exercise)
					)
					.ToList();
		}


		public byte[]? RetrieveExerciseImageBytes(int exerciseId)
		{
			return
				_repo
					.GetExerciseImageBytes(exerciseId);
		}


		public void CommitExercise
		(
			int exerciseId,
			string exerciseName,
			string? exerciseDescription,
			string? exerciseImageBase64,
			List<int> muscleIds
		)
		{
			Exercise exerciseDataToCommit =
				new Exercise
				{
					ExerciseName = exerciseName,
					ExerciseDesc = exerciseDescription,
					ExerciseImage =
						exerciseImageBase64 == null ?
							null :
							Convert.FromBase64String(exerciseImageBase64)
				};


			if (exerciseId != -1)
			{
				Exercise? trackedExercise = _repo.GetExerciseById(exerciseId);

				if(trackedExercise == null)
				{
					throw
						new HttpRequestException
						(
							$"An excercise with an id of '{exerciseId}' was not found to update.",
							null,
							HttpStatusCode.NotFound
						);
				}
				else
				{
					_repo
						.UpdateExercise
						(
							trackedExercise, 
							exerciseDataToCommit,
							muscleIds
						);
				}
			}
			else
			{
				_repo
					.CreateExercise
					(
						exerciseDataToCommit, 
						muscleIds
					);
			}
		}
		#endregion



		#region 'IWorkoutTrackerService' Implementation
		public ExerciseDto FromModel(Exercise model)
		{
			return
				new ExerciseDto
				{
					ExerciseId = model.ExerciseId,
					ExerciseName = model.ExerciseName,
					ExerciseDescription = model.ExerciseDesc,
					ExerciseImageBase64 = 
						model.ExerciseImage == null ?
							null :
							Convert
								.ToBase64String
								(
									model.ExerciseImage, 
									Base64FormattingOptions.None
								),
					Muscles =
						model
							.ExerciseMuscles
							.Select
							(
								em =>
									new MuscleDto
									{
										MuscleId = em.Muscle.MuscleId,
										SimpleName = em.Muscle.MuscleShortDesc,
										AnatomicalName = em.Muscle.MuscleLongDesc,
										MuscleGroupName = em.Muscle.MuscleGroup?.MuscleGroupDesc
									}
							)
							.ToList()
				};
		}
		#endregion
	}
}
