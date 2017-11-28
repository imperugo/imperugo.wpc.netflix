using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace imperugo.wpc.netflix.apis.Extensions
{
	internal static class ApplicationBuilderExtensions
	{
		public static void SeedDatabase(this IApplicationBuilder app)
		{
			var movieRepo = app.ApplicationServices.GetRequiredService<IMovieRepository>();
			
			var count = movieRepo.CountAsync().GetAwaiter().GetResult();

			if (count == 0)
			{
				var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
				
				using (var client = new HttpClient())
				{
					string response = client
						.GetStringAsync("http://www.theimdbapi.org/api/find/movie?title=star%20wars")
						.GetAwaiter()
						.GetResult();

					var movies = JsonConvert.DeserializeObject<Movie[]>(response);

					movieRepo.Collection.InsertMany(movies);

					logger.LogInformation($"Adding {movies.Length} movied to the database.");
				}
			}
		}
	}
}
