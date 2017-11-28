using System;
using System.Linq;
using imperugo.wpc.netflix.apis.Apis.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace imperugo.wpc.netflix.apis.Apis.v1
{
	/// <summary>
	/// Represents a RESTful utility service.
	/// </summary>
	[ApiVersion("1.0")]
	[ApiVersion("0.9", Deprecated = true)]
	[Route("v1.0/Utility/[action]")]
	public class UtilityController : ApiControllerBase
	{
        public UtilityController()
        {
        }

        /// <summary>
        /// Nothing special.
        /// </summary>
        /// <returns>A sample message.</returns>
        /// <response code="200">You are correctly logged in.</response>
        [HttpGet]
		[ProducesResponseType(typeof(SampleResponse), 200)]
		public IActionResult Public()
		{
			return Ok(new SampleResponse { Message = "This route is available for all clients." });
		}

		/// <summary>
		/// Check if your are correctly logged in.
		/// </summary>
		/// <returns>An instance of<see cref="SampleResponse"/>.</returns>
		/// <response code = "200" > You are correctly logged in.</response>
		/// <response code = "401" > You are not logged ing.</response>
		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(SampleResponse), 200)]
		[ProducesResponseType(401)]
		public IActionResult Protected()
		{
			return Json(new SampleResponse { Message = "This route is only for authenticated clients." });
		}

		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(SampleResponse), 200)]
		public IActionResult GetTimezoneIds()
		{
			return Ok(TimeZoneInfo.GetSystemTimeZones().Select(x => x.Id).ToArray());
		}
	}

	internal class SampleResponse
	{
		public string Message { get; set; }
	}
}