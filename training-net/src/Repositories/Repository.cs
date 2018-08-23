using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;

namespace training_net.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataBaseContext _context;
        public DataBaseContext Context
        {
            get { return this._context;}
        }
        public Repository(DataBaseContext context) => this._context = context;
        public T Get(int id) => Context.Set<T>().Find(id);
        public IEnumerable<T> GetAll() => Context.Set<T>().ToList();
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);
        public void Add(T entity) => Context.Set<T>().Add(entity);
        public void AddRange(IEnumerable<T> entities) => Context.Set<T>().AddRange(entities);
        public void Remove(T entity) => Context.Set<T>().Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => Context.Set<T>().RemoveRange(entities);
    }
}
