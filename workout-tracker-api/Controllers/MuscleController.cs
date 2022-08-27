using dto;
using Microsoft.AspNetCore.Mvc;
using service.Interfaces;

namespace workout_tracker_api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MuscleController : ControllerBase
	{
		#region Non-Public Member(s)
		private IMuscleService _service;
		#endregion



		#region Constructor(s)
		public MuscleController(IMuscleService service)
		{
			_service = service;
		}
		#endregion



		#region Controller Method(s)
		/// <summary>
		///		Retrieves a collection of objects describing 
		///		each muscle and its respective muscle group.
		/// </summary>
		/// <returns>
		///		A collection of all <seealso cref="MuscleDto"/>s.
		/// </returns>
		[HttpGet(Name = "GetMuscles")]
		public IEnumerable<MuscleDto> GetMuscles()
		{
			return 
				_service.RetrieveMuscles();
		}
		#endregion
	}
}
