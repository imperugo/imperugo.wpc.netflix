namespace imperugo.wpc.netflix.apis.Mongo
{
	public interface IMongoFactory
	{
		IMongoConnection GetConnection(string name);
	}
}