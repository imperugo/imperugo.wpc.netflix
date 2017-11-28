using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Mongo
{
	public interface IMongoConnection
	{
		IMongoClient Client { get; }
		IMongoDatabase Databse { get; }
		string Name { get; }
	}
}