using imperugo.wpc.netflix.apis.Results;
using Microsoft.AspNetCore.Mvc;

namespace imperugo.wpc.netflix.apis.Apis.Helpers
{
	public abstract class ApiControllerBase : Controller
	{
		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult Conflict()
		{
			return new ConflictResult();
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult Conflict(object error)
		{
			return new ConflictResultObjectResult(error);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult NotAcceptable()
		{
			return new NotAcceptable();
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public IActionResult NotAcceptable(object error)
		{
			return new NotAcceptableResultObjectResult(error);
		}
	}
}
