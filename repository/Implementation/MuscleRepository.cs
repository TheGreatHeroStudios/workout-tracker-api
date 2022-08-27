using Microsoft.EntityFrameworkCore;
using model;
using persistence.Context;
using repository.Interfaces;

namespace repository.Implementation
{
    public class MuscleRepository : WorkoutTrackerRepository, IMuscleRepository
    {
		#region Constructor(s)
		public MuscleRepository(WorkoutTrackerContext context) 
			: base(context)
		{
		}
		#endregion



		#region 'IMuscleRepository' Implementation
		public List<Muscle> GetMuscles()
		{
			return
				_context
					.Muscles
					.Include(m => m.MuscleGroup)
					.ToList();
		}
		#endregion
	}
}