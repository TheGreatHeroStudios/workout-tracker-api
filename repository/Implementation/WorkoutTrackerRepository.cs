using persistence.Context;
using repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Implementation
{
	public abstract class WorkoutTrackerRepository<TEntity>
	{
		protected WorkoutTrackerContext _context;

		public WorkoutTrackerRepository(WorkoutTrackerContext context)
		{
			_context = context;
		}

		protected abstract IQueryable<TEntity> ComposeQueryable();
	}
}
