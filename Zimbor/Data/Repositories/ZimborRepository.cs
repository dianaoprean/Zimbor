using Data.Contracts;
using Domain;
using System.Data.Objects.DataClasses;

namespace Data.Repositories
{
	/// <summary>
	/// IZimborRepository Interface . Provides the base functionality required by all entities
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IZimborRepository<T> : IRepository<T, ZimborEntities> where T : EntityObject
	{
		bool Insert(T entity);
		new bool Delete(T entity);
		void DeleteNoTransaction(T entity);
		new void AddOrAttach(T entity);
		void InsertNoTransaction(T Entity);
	}

	//<summary>
	//Generic repository class with the base functionality
	//</summary>
	//<typeparam name="T"></typeparam>
    public class Repository<T> : BaseRepository<T, ZimborEntities>, IZimborRepository<T> where T : EntityObject
	{
		public bool Insert(T entity)
		{
			using (var transaction = BeginTransaction())
			{
				AddOrAttach(entity);
				if (Save() == 0) return false;
				transaction.Commit();
			}
			return true;
		}

		new public bool Delete(T entity)
		{
			using (var transaction = BeginTransaction())
			{
				base.Delete(entity);
				if (Save() == 0) return false;
				transaction.Commit();
			}
			return true;

		}

		public void DeleteNoTransaction(T entity)
		{
			base.Delete(entity);
		}

		public void InsertNoTransaction(T entity)
		{
			AddOrAttach(entity);
		}
	}
}
