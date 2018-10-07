using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RC_Charter2.Repositories
{
	public interface IRepository<T> where T : class, new()
	{
		void Add(T item);
		void Remove(T item);
		void Update(T item);
		IQueryable<T> Query();
		void AddRange(IEnumerable<T> items);
		void RemoveRange(IEnumerable<T> items);
		bool SaveChanges();
		T Get(Expression<Func<T, bool>> expression);
		IList<T> GetRange(Expression<Func<T, bool>> expression);
	}
}
