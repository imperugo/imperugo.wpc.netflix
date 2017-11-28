using System.Threading.Tasks;
using Humanizer;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Mongo
{
	public static class MongoDatabaseExtensions
	{
		public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase db, string name = null)
		{
			if (name == null)
			{
				name = typeof(T).Name.Pluralize().ToLowerInvariant();
			}

			return db.GetCollection<T>(name);
		}

		public static Task DropCollectionAsync<T>(this IMongoDatabase db, string name = null)
		{
			if (name == null)
			{
				name = typeof(T).Name.Pluralize().ToLowerInvariant();
			}

			return db.DropCollectionAsync(name);
		}
	}
}