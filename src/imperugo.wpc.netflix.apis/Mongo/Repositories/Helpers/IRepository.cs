using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using imperugo.wpc.netflix.apis.Mongo.Documents;
using MongoDB.Driver;

namespace imperugo.wpc.netflix.apis.Mongo.Repositories.Helpers
{
	/// <summary>
	///     IRepository definition.
	/// </summary>
	/// <typeparam name="T">The type contained in the repository.</typeparam>
	public interface IRepository<T> where T : DocumentBase
	{
		IMongoCollection<T> Collection { get; }

		/// <summary>
		/// Saves the or update.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		Task<ReplaceOneResult> SaveOrUpdateAsync(Expression<Func<T, bool>> predicate, T entity);

		/// <summary>
		/// Check if a document with the specified predicate exists into the store
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
		bool Exist(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Check if a document with the specified predicate exists into the store
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
		Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Check if a document with the specified id exists into the store
		/// </summary>
		/// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
		/// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
		bool Exist(string id);

		/// <summary>
		/// Check if a document with the specified id exists into the store
		/// </summary>
		/// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
		/// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
		Task<bool> ExistAsync(string id);

		/// <summary>
		/// Saves the or update.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		ReplaceOneResult SaveOrUpdate(Expression<Func<T, bool>> predicate, T entity);

		/// <summary>
		/// Saves the or update.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		Task<ReplaceOneResult> SaveOrUpdateAsync(T entity);

		/// <summary>
		/// Saves the or update.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		ReplaceOneResult SaveOrUpdate(T entity);

		/// <summary>
		///     Returns the T by its given id.
		/// </summary>
		/// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
		/// <returns>The Entity T.</returns>
		T GetById(string id);

		/// <summary>
		///     Returns the T by its given id.
		/// </summary>
		/// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
		/// <returns>The Entity T.</returns>
		Task<T> GetByIdAsync(string id);

		/// <summary>
		///     Adds the new entity in the repository.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <returns>The added entity including its new ObjectId.</returns>
		Task<T> AddAsync(T entity);

		/// <summary>
		///     Adds the new entities in the repository.
		/// </summary>
		/// <param name="entities">The entities of type T.</param>
		Task AddAsync(IEnumerable<T> entities);

		/// <summary>
		///     Upserts an entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The updated entity.</returns>
		Task<T> UpdateAsync(T entity);

		/// <summary>
		///     Upserts the entities.
		/// </summary>
		/// <param name="entities">The entities to update.</param>
		Task UpdateAsync(IEnumerable<T> entities);

		/// <summary>
		///     Deletes an entity from the repository by its id.
		/// </summary>
		/// <param name="id">The entity's id.</param>
		void Delete(string id);

		/// <summary>
		///     Deletes an entity from the repository by its id.
		/// </summary>
		/// <param name="id">The entity's id.</param>
		Task DeleteAsync(string id);

		/// <summary>
		///     Deletes the given entity.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		Task DeleteAsync(T entity);

		/// <summary>
		///     Deletes all entities in the repository.
		/// </summary>
		Task DeleteAllAsync();

		/// <summary>
		///     Counts the total entities in the repository.
		/// </summary>
		/// <returns>Count of entities in the repository.</returns>
		Task<long> CountAsync();

		long GetNextSequence();

		Task<long> GetNextSequenceAsync();
	}
}