using System.Collections.Generic;
using System.Linq;

namespace imperugo.wpc.netflix.apis.Mongo
{
	public class MongoFactory : IMongoFactory
	{
		private readonly IEnumerable<IMongoConnection> connections;

		public MongoFactory(IEnumerable<IMongoConnection> connections)
		{
			this.connections = connections;
		}

		public IMongoConnection GetConnection(string name)
		{
			return this.connections.FirstOrDefault(x => x.Name == name);
		}
	}
}