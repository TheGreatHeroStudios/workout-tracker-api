using dto;
using model;
using repository.Interfaces;
using service.Interfaces;

namespace service.Implementation
{
    public class MuscleService : IMuscleService
    {
		#region Non-Public Member(s)
		private IMuscleRepository _repo;
		#endregion



		#region Constructor(s)
		public MuscleService(IMuscleRepository repo)
		{
			_repo = repo;
		}
		#endregion



		#region 'IMuscleService' Implementation
		public List<MuscleDto> RetrieveMuscles()
		{
			return
				_repo
					.GetMuscles()
					.Select
					(
						muscle => FromModel(muscle)
					)
					.ToList();
		}
		#endregion



		#region 'IWorkoutTrackerService' Implementation
		public MuscleDto FromModel(Muscle model)
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
		#endregion
	}
}