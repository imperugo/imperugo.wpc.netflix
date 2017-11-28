using MongoDB.Bson;

namespace imperugo.wpc.netflix.apis.Mongo
{
	public interface IMongoHelper
	{
		string NewId();
	}

	public class MongoHelper : IMongoHelper
	{
		public string NewId()
		{
			return ObjectId.GenerateNewId().ToString();
		}
	}
}