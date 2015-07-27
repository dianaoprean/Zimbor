using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
	/// <summary>
	/// Repository Interface defines the base functionality required by all Repositories.
	/// </summary>
	public interface IRepository<TEntity, TContext>
	{
		/// <summary>
		/// Gets or sets the current context
		/// </summary>
		TContext Context { get; set; }

		/// <summary>
		/// Start Transaction
		/// </summary>
		/// <returns>Returns current opened transaction</returns>
		DbTransaction BeginTransaction();

		/// <summary>
		/// Insert new data into context
		/// </summary>
		/// <param name="entity">TEntity</param>
		void Add(TEntity entity);

		/// <summary>
		/// Insert if new otherwise attach data into context
		/// </summary>
		void AddOrAttach(TEntity entity);

		/// <summary>
		/// Delete all related entries
		/// </summary>
		/// <param name="entity">TEntity</param>
		void DeleteRelatedEntries(TEntity entity);

		/// <summary>
		/// Delete the given TEntity
		/// </summary>
		/// <param name="entity">TEntity</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Save context changes
		/// </summary>
		/// <returns>0 if no changes were saved, otherwise number of saved changes </returns>
		int Save();

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		ObjectQuery<TEntity> Query();

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		IQueryable<TEntity> Select();

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpressions">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		IQueryable<TEntity> Select(List<string> sortExpressions);

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpression">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		IQueryable<TEntity> Select(string sortExpression);

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpression">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		IQueryable<TEntity> Select<T>(Expression<Func<TEntity, T>> sortExpression);

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where);

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where, List<string> sortExpressions);

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where, string sortExpression);

		

		/// <summary>
		/// Gets the TEntity based on given id
		/// </summary>
		/// <param name="keyValue">Key Value</param>
		/// <returns>Return TEtity based on given id; null if does not exist</returns>
		TEntity GetByKeyValue(object keyValue);

		/// <summary>
		/// Get Count of all records
		/// </summary>
		/// <returns>count of all records</returns>
		int GetCount();

		/// <summary>
		/// Get Count of all records that match the given where
		/// </summary>
		/// <returns>count of all records</returns>
		int GetCount(Expression<Func<TEntity, bool>> where);

		IEnumerable<TEntity> Find(string whereClause);
		IEnumerable<TEntity> Find(string whereClause, string orderByClause);
		IEnumerable<TEntity> Find(string whereClause, string orderByClause, int pageNumber, int pageSize);
	}
}
