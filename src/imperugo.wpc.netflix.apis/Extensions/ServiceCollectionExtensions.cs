using imperugo.wpc.netflix.apis.Configuration;
using imperugo.wpc.netflix.apis.Mongo;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Extensions
{
	internal static class ServiceCollectionExtensions
	{
		public static void AddMongoDb(this IServiceCollection services)
		{
			var databaseConfiguration = services.BuildServiceProvider().GetRequiredService<MongoDbDatabaseConfiguration>();

			services.AddSingleton<IMongoHelper, MongoHelper>();
			services.AddSingleton<IMongoFactory, MongoFactory>();
			services.AddSingleton<IMovieRepository, MovieRepository>();

			// We could have more than one db
			var client1 = new MongoClient(databaseConfiguration.ConnectionString);
			var db1 = client1.GetDatabase(databaseConfiguration.DatabaseName);
			
			IMongoConnection remarketingConnection = new MongoConnection(client1, db1, "netflix");
			services.AddSingleton(remarketingConnection);

			// Serializer
			BsonSerializer.RegisterSerializer(new DecimalSerializer().WithRepresentation(BsonType.Double));

			BsonClassMap.RegisterClassMap<DocumentBase>(cm => {
				cm.AutoMap();
				cm.MapIdProperty(c => c.Id)
					.SetIdGenerator(StringObjectIdGenerator.Instance)
					.SetSerializer(new StringSerializer(BsonType.ObjectId));
			});
		}
	}
}
