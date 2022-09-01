using dto;
using model;

namespace service.Interfaces
{
	public interface IMuscleService : IWorkoutTrackerService<Muscle, MuscleDto>
	{
		public List<MuscleDto> RetrieveMuscles();
	}
}
