using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace imperugo.wpc.netflix.apis.Results
{
	public class NotAcceptable : StatusCodeResult
	{
		/// <summary>
		///     Creates a new <see cref="ConflictResult" /> instance.
		/// </summary>
		public NotAcceptable()
			: base(StatusCodes.Status406NotAcceptable)
		{
		}
	}

	public class NotAcceptableResultObjectResult : ObjectResult
	{
		/// <summary>
		/// Creates a new <see cref="ConflictResultObjectResult"/> instance.
		/// </summary>
		/// <param name="error">Contains the errors to be returned to the client.</param>
		public NotAcceptableResultObjectResult(object error)
			: base(error)
		{
			StatusCode = StatusCodes.Status406NotAcceptable;
		}

		/// <summary>
		/// Creates a new <see cref="ConflictResultObjectResult"/> instance.
		/// </summary>
		/// <param name="modelState"><see cref="ModelStateDictionary"/> containing the validation errors.</param>
		public NotAcceptableResultObjectResult(ModelStateDictionary modelState)
			: base(new SerializableError(modelState))
		{
			if (modelState == null)
			{
				throw new ArgumentNullException(nameof(modelState));
			}

			StatusCode = StatusCodes.Status406NotAcceptable;
		}
	}
}