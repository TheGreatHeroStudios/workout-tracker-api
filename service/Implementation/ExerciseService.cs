using dto;
using model;
using repository.Interfaces;
using service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
