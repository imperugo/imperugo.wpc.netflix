using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using imperugo.wpc.netflix.apis.Mongo.Extensions;
using MongoDB.Driver;
using Sequence = imperugo.wpc.netflix.apis.Mongo.Documents.Sequence;

namespace imperugo.wpc.netflix.apis.Mongo.Repositories.Helpers
{
	public class RepositoryBase<T> : IRepository<T> where T : DocumentBase
	{
		public RepositoryBase(IMongoFactory factory, string name, string collectionName = null)
		{
			Database = factory.GetConnection(name).Databse;

			Collection = MongoDatabaseExtensions.GetCollection<T>(Database, collectionName);
		}

		protected internal IMongoDatabase Database { get; }
		public IMongoCollection<T> Collection { get; }

		public bool Exist(Expression<Func<T, bool>> predicate)
		{
			return Collection.Find(predicate).Count() > 0;
		}

		public Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
		{
			return Task.FromResult(Exist(predicate));
		}

		public bool Exist(string id)
		{
			return Collection.Find(x => x.Id == id).Count() > 0;
		}

		public Task<bool> ExistAsync(string id)
		{
			return Task.FromResult(Exist(id));
		}

		public virtual ReplaceOneResult SaveOrUpdate(Expression<Func<T, bool>> predicate, T entity)
		{
			return Collection.ReplaceOne(predicate, entity, new UpdateOptions { IsUpsert = true });
		}

		public virtual Task<ReplaceOneResult> SaveOrUpdateAsync(Expression<Func<T, bool>> predicate, T entity)
		{
			return Collection.ReplaceOneAsync(predicate, entity, new UpdateOptions { IsUpsert = true });
		}

		public virtual Task<ReplaceOneResult> SaveOrUpdateAsync(T entity)
		{
			return Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
		}

		public virtual ReplaceOneResult SaveOrUpdate(T entity)
		{
			return Collection.ReplaceOne(x => x.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
		}

		public T GetById(string id)
		{
			return Find(x => x.Id == id);
		}

		public virtual Task<T> GetByIdAsync(string id)
		{
			return FindAsync(x => x.Id == id);
		}

		public virtual async Task<T> AddAsync(T entity)
		{
			await Collection.InsertOneAsync(entity);

			return entity;
		}

		public virtual Task AddAsync(IEnumerable<T> entities)
		{
			return Collection.InsertManyAsync(entities);
		}

		public virtual async Task<T> UpdateAsync(T entity)
		{
			await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

			return entity;
		}

		public virtual Task UpdateAsync(IEnumerable<T> entities)
		{
			var tasks = new List<Task>();

			foreach (var entity in entities)
			{
				var t = UpdateAsync(entity);

				tasks.Add(t);
			}

			return Task.WhenAll(tasks);
		}

		public void Delete(string id)
		{
			Collection.DeleteOne(x => x.Id == id);
		}

		public virtual Task DeleteAsync(string id)
		{
			return Collection.DeleteOneAsync(x => x.Id == id);
		}

		public virtual Task DeleteAsync(T entity)
		{
			return DeleteAsync(entity.Id);
		}

		public virtual Task DeleteAllAsync()
		{
			return Database.DropCollectionAsync<T>();
		}

		public virtual Task<long> CountAsync()
		{
			return Collection.Find(x => x.Id != null).Limit(1).CountAsync();
		}

		public long GetNextSequence()
		{
			var udate = Builders<Sequence>.Update.Inc(x => x.Value, 1);
			var filter = Builders<Sequence>.Filter.Eq(x => x.Id, typeof(T).Name);

			var sequence = Database
				.GetCollection<Sequence>()
				.FindOneAndUpdate(
					filter,
					udate,
					new FindOneAndUpdateOptions<Sequence>
					{
						IsUpsert = true
					});


			// In case the sequence doesn't exist yer into the collection
			if (sequence == null)
				return 1;

			return sequence.Value + 1;
		}

		public async Task<long> GetNextSequenceAsync()
		{
			var udate = Builders<Sequence>.Update.Inc(x => x.Value, 1);
			var filter = Builders<Sequence>.Filter.Eq(x => x.Id, typeof(T).Name);

			var sequence = await Database
				.GetCollection<Sequence>()
				.FindOneAndUpdateAsync(
					filter,
					udate,
					new FindOneAndUpdateOptions<Sequence>
					{
						IsUpsert = true
					});


			// In case the sequence doesn't exist yer into the collection
			if (sequence == null)
				return 1;

			return sequence.Value + 1;
		}

		protected virtual Task DeleteAsync(Expression<Func<T, bool>> predicate)
		{
			return Collection.DeleteOneAsync(predicate);
		}

		protected virtual T Find(Expression<Func<T, bool>> predicate)
		{
			return Collection.Find(predicate).FirstOrDefault();
		}

		protected virtual Task<T> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return Collection.Find(predicate).FirstOrDefaultAsync();
		}

		protected virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await Collection.Find(predicate).Limit(1).CountAsync() > 0;
		}
	}
}