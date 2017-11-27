using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace imperugo.wpc.netflix.apis.Results
{
	/// <summary>
	///     A <see cref="StatusCodeResult" /> that when
	///     executed will produce a Conflict (409) response.
	/// </summary>
	public class ConflictResult : StatusCodeResult
	{
		/// <summary>
		///     Creates a new <see cref="ConflictResult" /> instance.
		/// </summary>
		public ConflictResult()
			: base(StatusCodes.Status409Conflict)
		{
		}
	}

	/// <summary>
	/// An <see cref="ObjectResult"/> that when executed will produce a Bad Request (400) response.
	/// </summary>
	public class ConflictResultObjectResult : ObjectResult
	{
		/// <summary>
		/// Creates a new <see cref="ConflictResultObjectResult"/> instance.
		/// </summary>
		/// <param name="error">Contains the errors to be returned to the client.</param>
		public ConflictResultObjectResult(object error)
            : base(error)
        {
			StatusCode = StatusCodes.Status409Conflict;
		}

		/// <summary>
		/// Creates a new <see cref="ConflictResultObjectResult"/> instance.
		/// </summary>
		/// <param name="modelState"><see cref="ModelStateDictionary"/> containing the validation errors.</param>
		public ConflictResultObjectResult(ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
			if (modelState == null)
			{
				throw new ArgumentNullException(nameof(modelState));
			}

			StatusCode = StatusCodes.Status400BadRequest;
		}
	}

	
}