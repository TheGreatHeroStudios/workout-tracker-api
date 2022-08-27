using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.Interfaces
{
	public interface IExerciseRepository
	{
		public Exercise? GetExerciseById(int exerciseId);
		public byte[]? GetExerciseImageBytes(int exerciseId);
		public List<Exercise> GetExercises(int pageIndex, int count);
	}
}
