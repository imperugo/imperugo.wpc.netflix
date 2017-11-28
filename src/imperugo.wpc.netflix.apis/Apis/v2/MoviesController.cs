using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Apis.Helpers;
using imperugo.wpc.netflix.apis.Apis.v2.Requests;
using imperugo.wpc.netflix.apis.Apis.v2.Responses;
using imperugo.wpc.netflix.apis.Attributes;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using imperugo.wpc.netflix.apis.Mongo.Extensions;

namespace imperugo.wpc.netflix.apis.Apis.v2
{
	[ApiVersion("2.0")]
	[Route("v2.0/Movies/[action]")]
	public class MoviesController : ApiControllerBase
	{
		private readonly IMovieRepository movieRepository;

		public MoviesController(IMovieRepository movieRepository)
		{
			this.movieRepository = movieRepository;
		}

		[HttpGet]
		[ValidateModel]
		[ProducesResponseType(typeof(List<Movie>), 200)]
		public Task<PagedResult<Movie>> GetMovies(SimplePagedRequest request)
		{
			return this.movieRepository
							.Collection
							.ToPagedResult(Builders<Movie>.Filter.Empty, request);
		}
	}
}
