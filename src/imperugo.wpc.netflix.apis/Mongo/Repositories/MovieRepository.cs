using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Repositories.Helpers;

namespace imperugo.wpc.netflix.apis.Mongo.Repositories
{
	public interface IMovieRepository : IRepository<Movie>
	{
		
	}

    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
	{
		public MovieRepository(IMongoFactory factory) : base(factory, "netflix")
		{
		}
	}
}
