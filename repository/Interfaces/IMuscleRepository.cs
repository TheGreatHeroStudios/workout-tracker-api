using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Interfaces
{
	public interface IMuscleRepository
	{
		Muscle GetMuscle(int muscleId);
		List<Muscle> GetMuscles();
	}
}
