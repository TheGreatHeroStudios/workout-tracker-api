using dto;
using Microsoft.AspNetCore.Mvc;
using service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace workout_tracker_api.Controllers
{
	
	public class ExerciseController : WorkoutTrackerController
	{
		#region Non-Public Member(s)
		private IExerciseService _service;
		#endregion



		#region Constructor(s)
		public ExerciseController(IExerciseService service)
		{
			_service = service;
		}
		#endregion



		#region 'Get' Endpoint(s)
		/// <summary>
		///		Retrieves a collection of exercises detailing
		///		the name, process, and affected muscles.
		/// </summary>
		/// <param name="pageIndex">
		///		Determines how many exercises to skip over when paging.
		///		The number of records is equal to the count * pageIndex.
		/// </param>
		/// <param name="count">
		///		The number of exercises to retrieve.
		/// </param>
		/// <returns>
		///		A collection of <seealso cref="ExerciseDto"/>s
		/// </returns>
		[HttpGet(Name = "GetExercises")]
		public IActionResult GetExercises
		(
			[FromQuery][Required] int pageIndex = 0,
			[FromQuery][Required] int count = 10
		)
		{
			return
				RunWithinErrorBoundary
				(
					() =>
						Ok
						(
							_service
								.RetrieveExercises(pageIndex, count)
						)
				);
		}


		/// <summary>
		///		Retrieves the image associated with an exercise
		/// </summary>
		/// <param name="exerciseId">
		///		The id of the exercise whose image should be retrieved.
		/// </param>
		/// <returns>
		///		The retrieved image (read from bytes).
		/// </returns>
		/*[HttpGet("Image", Name = "GetExerciseImage")]
		public IActionResult GetExerciseImage
		(
			[FromQuery][Required] int exerciseId
		)
		{
			byte[]? imageBytes = 
				_service.RetrieveExerciseImageBytes(exerciseId);

			if(imageBytes?.Any() ?? false)
			{
				return File(imageBytes, "image/png");
			}
			else
			{
				return NotFound();
			}
		}*/
		#endregion



		#region 'Put' Endpoint(s)
		[HttpPut(Name = "PutExercise")]
		public IActionResult PutExercise
		(
			[FromBody][Required] ExerciseUpdatePayload updatePayload
		)
		{
			return
				RunWithinErrorBoundary
				(
					() =>
					{
						_service
							.CommitExercise
							(
								updatePayload.exerciseId ?? -1,
								updatePayload.exerciseName,
								updatePayload.exerciseDescription,
								updatePayload.exerciseImageBase64,
								updatePayload.muscleIds
							);

						return Ok();
					}
				);
		}
		#endregion
	}
}
