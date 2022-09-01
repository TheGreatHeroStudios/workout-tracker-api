using Microsoft.AspNetCore.Mvc;

namespace workout_tracker_api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public abstract class WorkoutTrackerController : ControllerBase
	{
		#region Non-Public Method(s)
		protected IActionResult RunWithinErrorBoundary(Func<IActionResult> logic)
		{
			try
			{
				return logic();
			}
			catch (HttpRequestException httpEx)
			{
				return
					StatusCode
					(
						(int?)httpEx.StatusCode ?? 500,
						httpEx.Message ?? "Unknown Server Error"
					);
			}
			catch (Exception ex)
			{
				return
					StatusCode
					(
						500,
						ex.Message ?? "Unknown Server Error"
					);
			}
		}
		#endregion
	}
}
