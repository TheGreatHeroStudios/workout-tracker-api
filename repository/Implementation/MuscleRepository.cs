using Microsoft.EntityFrameworkCore;
using model;
using persistence.Context;
using repository.Interfaces;

namespace repository.Implementation
{
    public class MuscleRepository : WorkoutTrackerRepository<Muscle>, IMuscleRepository
    {
		#region Constructor(s)
		public MuscleRepository(WorkoutTrackerContext context) 
			: base(context)
		{
		}
		#endregion



		#region 'IMuscleRepository' Implementation
		public Muscle GetMuscle(int muscleId)
		{
			return
				ComposeQueryable()
					.Single
					(
						muscle =>
							muscle.MuscleId == muscleId
					);
		}


		public List<Muscle> GetMuscles()
		{
			return
				ComposeQueryable().ToList();
		}
		#endregion



		#region Override(s)
		protected override IQueryable<Muscle> ComposeQueryable() =>
			_context
				.Muscles
				.Include(m => m.MuscleGroup);
		#endregion
	}
}