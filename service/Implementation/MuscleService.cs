using dto;
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
						muscle =>
							new MuscleDto
							{
								MuscleId = muscle.MuscleId,
								AnatomicalName = muscle.MuscleLongDesc,
								SimpleName = muscle.MuscleShortDesc,
								MuscleGroupName = muscle.MuscleGroup.MuscleGroupDesc
							}
					)
					.ToList();
		}
		#endregion
	}
}