using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RC_Charter2.Repositories
{
	public class EfRepository<T> : IRepository<T> where T: class, new()
	{
		private DbContext _context;

		public EfRepository(DbContext context)
		{
			_context = context;
		}

		public void Add(T item)
		{
			_context.Entry(item).State = EntityState.Added;
		}

		public void Remove(T item)
		{
			_context.Entry(item).State = EntityState.Deleted;
		}

		public void Update(T item)
		{
			_context.Entry(item).State = EntityState.Modified;
		}

		public IQueryable<T> Query()
		{
			return _context.Set<T>();
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (var i in items)
			{
				_context.Entry(i).State = EntityState.Added;
			}
		}

		public void RemoveRange(IEnumerable<T> items)
		{
			foreach (var i in items)
			{
				_context.Entry(i).State = EntityState.Deleted;
			}
		}

		private bool _isUpdating;
		private T _itemToUpdate;

		public bool SaveChanges()
		{
			try
			{
				var result = _context.SaveChanges();
				return result >= 0;
			}
			catch (Exception e)
			{
				if (_isUpdating)
				{
					_context.Entry(_itemToUpdate).State = EntityState.Detached;
					throw new Exception($"There was an error in updating a record.");
				}

				throw e;
			}
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().FirstOrDefault(expression);
		}

		public IList<T> GetRange(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).ToList();
		}
	}
}
