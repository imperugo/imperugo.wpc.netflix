using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Apis.v2.Requests;
using imperugo.wpc.netflix.apis.Apis.v2.Responses;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Mongo.Extensions
{
	public static class MongoDbQueryExtensions
	{
		public static IFindFluent<TDocument, TDocument> Page<TDocument>(this IFindFluent<TDocument, TDocument> find, SimplePagedRequest request)
		{
			return find
				.Limit(request.PageSize)
				.Skip(request.PageIndex*request.PageSize);
		}

		public static async Task<PagedResult<TDocument>> ToPagedResult<TDocument>(
			this IMongoCollection<TDocument> collection,
			FilterDefinition<TDocument> filter,
			SimplePagedRequest request,
			SortDefinition<TDocument> sort = null) where TDocument : DocumentBase
		{
			if (sort == null)
			{
				sort = Builders<TDocument>.Sort.Descending(x => x.Id);
			}

			if (filter == null)
			{
				filter = Builders<TDocument>.Filter.Empty;
			}

			var result = collection
				.Find(filter)
				.Sort(sort)
				.Limit(request.PageSize)
				.Skip(request.PageIndex * request.PageSize)
				.ToListAsync();

			var count = collection
				.Find(filter)
				.CountAsync();

			await Task.WhenAll(result, count);

			return new PagedResult<TDocument>(request.PageIndex, request.PageSize, result.Result, count.Result);
		}
	}
}