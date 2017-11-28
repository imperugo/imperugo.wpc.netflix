using System;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Mongo
{
	public class MongoConnection : IMongoConnection
	{
		public MongoConnection(IMongoClient client, IMongoDatabase databse, string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Invalid connectin name.", nameof(name));
			}

			Client = client;
			Databse = databse;
			Name = name;
		}

		public IMongoClient Client { get; }
		public IMongoDatabase Databse { get; }
		public string Name { get; }
	}
}