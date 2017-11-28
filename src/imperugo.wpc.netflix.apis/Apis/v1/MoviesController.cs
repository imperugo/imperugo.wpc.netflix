using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Apis.Helpers;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Apis.v1
{
	[ApiVersion("1.0")]
	[ApiVersion("0.9", Deprecated = true)]
	[Route("v1.0/Movies/[action]")]
	public class MoviesController : ApiControllerBase
	{
		private readonly IMovieRepository movieRepository;

		public MoviesController(IMovieRepository movieRepository)
		{
			this.movieRepository = movieRepository;
		}

		[HttpGet]
		[ProducesResponseType(typeof(List<Movie>), 200)]
		public Task<List<Movie>> GetMovies()
		{
			return this.movieRepository.Collection.Find(Builders<Movie>.Filter.Empty).ToListAsync();
		}
	}
}
