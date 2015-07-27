using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using Domain;
using Data.Contracts;
using Infrastructure.Extensions;

namespace Data.Repositories
{
	/// <summary>
	/// IRepository Interface implementation. Provides the base functionality required by all entities
	/// </summary>
	public class BaseRepository<TEntity, TContext> : IRepository<TEntity, TContext>
		where TEntity : EntityObject
		where TContext : ObjectContext
	{
		#region Properties

		[Dependency]
		public TContext Context { get; set; }

		public int PageSize { get; set; }


		#endregion

		public BaseRepository()
		{

		}

		#region IRepository<TE,TC> Members

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public ObjectQuery<TEntity> Query()
		{
			return Context.Query<TEntity>();
		}

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public IQueryable<TEntity> Select()
		{
			return Context.Query<TEntity>().AsQueryable();
		}

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpressions">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		public IQueryable<TEntity> Select(List<string> sortExpressions)
		{
			var query = Select();
			if (sortExpressions == null || !sortExpressions.Any()) return query;
			sortExpressions.ForEach(sortExpression => query = query.OrderBy(sortExpression));
			return query;
		}

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpression">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		public IQueryable<TEntity> Select(string sortExpression)
		{
			return Select(new List<string> { sortExpression });
		}

		/// <summary>
		/// Select All Entity in sorted Order
		/// </summary>
		/// <param name="sortExpression">Sort Expression/condition</param>
		/// <returns>Collection of TEntity</returns>
		public IQueryable<TEntity> Select<T>(Expression<Func<TEntity, T>> sortExpression)
		{
			if (null == sortExpression) return Select();
			return Context.Query<TEntity>().OrderBy(sortExpression).AsQueryable();
		}

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where)
		{
			return Context.Query<TEntity>().Where(where).AsQueryable();
		}

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where, List<string> sortExpressions)
		{
			var query = Select(where);
			if (sortExpressions == null || !sortExpressions.Any()) return query;
			sortExpressions.ForEach(sortExpression => query = query.OrderBy(sortExpression));
			return query;
		}

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> where, string sortExpression)
		{
			return Select(where, new List<string> { sortExpression });
		}

		/// <summary>
		/// A generic method to return ALL the entities of type TEntity based on the given where clause.
		/// </summary>
		/// <returns>Returns a set of TEntity.</returns>
		public IQueryable<TEntity> Select<T>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, T>> sortExpression)
		{
			if (null == sortExpression) return Select(where);
			return Context.Query<TEntity>().OrderBy(sortExpression).AsQueryable();
		}


		/// Get Count of all records
		/// </summary>
		/// <returns>count of all records</returns>
		public int GetCount()
		{
			return Context.Query<TEntity>().Count();
		}

		/// <summary>
		/// Get Count of all records that match the given where
		/// </summary>
		/// <returns>count of all records</returns>
		public int GetCount(Expression<Func<TEntity, bool>> where)
		{
			if (where == null) return GetCount();
			var query = Context.Query<TEntity>();
			var querry = query.Where(where);
			return querry.Count();
		}

		public bool HasAny()
		{
			return Context.Query<TEntity>().Any();
		}

		public bool HasAny(Expression<Func<TEntity, bool>> where)
		{
			if (where == null) return HasAny();
			return Context.Query<TEntity>().Any(where);
		}

		/// <summary>
		/// Delete data from context
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		public void Delete(TEntity entity)
		{
			Context.DeleteObject(entity);
		}

		/// <summary>
		/// Insert new data into context
		/// </summary>
		public void Add(TEntity entity)
		{
			Context.AddObject(Context.GetEntitySetName<TEntity>(), entity);
		}

		/// <summary>
		/// Insert if new otherwise attach data into context
		/// </summary>
		public void AddOrAttach(TEntity entity)
		{
			// Define an ObjectStateEntry and EntityKey for the current object.
			// Get the detached object's entity key.
			var key = entity.EntityKey ?? Context.CreateEntityKey(Context.GetEntitySetName<TEntity>(), entity);

			// Get the original item based on the entity key from the context
			// or from the database.
			object originalItem;
			if (!Context.TryGetObjectByKey(key, out originalItem))
				Add(entity);
			else if (originalItem is EntityObject && ((EntityObject)originalItem).EntityState != EntityState.Added)
			{
				var state = ((EntityObject)entity).EntityState;
				if (state == EntityState.Detached)
				{
					Context.Attach(entity);
				}
				Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);

				// Call the ApplyPropertyChanges method to apply changes
				// from the updated item to the original version.
				Context.ApplyCurrentValues(key.EntitySetName, entity);
			}
		}



		/// <summary>
		/// Start Transaction
		/// </summary>
		/// <returns>Returns current opened transaction</returns>
		public DbTransaction BeginTransaction()
		{
			if (Context.Connection.State != ConnectionState.Open)
				Context.Connection.Open();
			return Context.Connection.BeginTransaction();
		}

		/// <summary>
		/// Save context changes
		/// </summary>
		/// <returns>0 if no changes were saved, otherwise number of saved changes </returns>
		public int Save()
		{
			try
			{
				return Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// Delete all related entries
		/// </summary>
		/// <param name="entity"></param>
		public void DeleteRelatedEntries(TEntity entity)
		{
			var entityWithRelationships =
				((IEntityWithRelationships)entity).RelationshipManager.GetAllRelatedEnds().SelectMany(
					re => re.CreateSourceQuery().OfType<EntityObject>()).Distinct();
			foreach (var relatedEntity in entityWithRelationships.ToArray())
				Context.DeleteObject(relatedEntity);
		}

		/// <summary>
		/// Gets the TEntity based on given id
		/// </summary>
		/// <param name="keyValue">Key Value</param>
		/// <returns>Return TEtity based on given id; null if does not exist</returns>
		public TEntity GetByKeyValue(object keyValue)
		{
			var xParam = Expression.Parameter(typeof(TEntity), Context.GetEntitySetName<TEntity>());
			var binaryExpr = Expression.Equal(
				Expression.Property(xParam, "Id"),
				Expression.Constant(keyValue));
			var resultCollection = Select(Expression.Lambda<Func<TEntity, bool>>(binaryExpr, new[] { xParam }));
			if (resultCollection != null && resultCollection.Count() > 0) return resultCollection.First();
			return null;
		}

		public virtual IEnumerable<TEntity> Find(string whereClause)
		{
			return Context.Query<TEntity>().Where(whereClause).ToList();
		}
		public virtual IEnumerable<TEntity> Find(string whereClause, string orderByClause)
		{
			var query = Context.Query<TEntity>().AsQueryable();
			if (string.IsNullOrEmpty(orderByClause))
				return query.ToList();
			if (string.IsNullOrEmpty(whereClause))
				return query.OrderBy(orderByClause).ToList();
			return query.Where(whereClause).OrderBy(orderByClause).ToList();
		}
		public virtual IEnumerable<TEntity> Find(string whereClause, string orderByClause, int pageNumber, int pageSize)
		{
			var query = Context.Query<TEntity>().AsQueryable();
			if (string.IsNullOrEmpty(whereClause))
				return query.OrderBy(orderByClause).Skip((pageNumber) * pageSize).Take(pageSize).ToList();
			return query.Where(whereClause).OrderBy(orderByClause).Skip((pageNumber) * pageSize).Take(pageSize).ToList();
		}

		#endregion
	}
}
