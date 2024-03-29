﻿using WebAPI.Data;
using WebAPI.Helpers.Interfaces;

namespace WebAPI.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

		public void Add<T>(T entity)
			  where T : class
		{
			var set = _context.Set<T>();
			set.Add(entity);
		}

		public void Attach<T>(T entity)
			where T : class
		{
			var set = _context.Set<T>();
			set.Attach(entity);
		}

		void IUnitOfWork.Remove<T>(T entity)
		{
			var set = _context.Set<T>();
			set.Remove(entity);
		}

		public void Update<T>(T entity)
			where T : class
		{
			var set = _context.Set<T>();
			set.Attach(entity);
			//_context.Entry(entity).State = EntityState.Modified; 
		}

		public IQueryable<T> Query<T>()
			where T : class
		{
			return _context.Set<T>();
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context = null;
		}
	}
}
