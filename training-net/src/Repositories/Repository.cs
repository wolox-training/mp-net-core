using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;

namespace training_net.Repositories
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataBaseContext Context;
        public Repository(DataBaseContext context) => this.Context = context;
        public TEntity Get(int id) => this.Context.Set<TEntity>().Find(id);
        public IEnumerable<TEntity> GetAll() => this.Context.Set<TEntity>().ToList();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => this.Context.Set<TEntity>().Where(predicate);
        public void Add(TEntity entity) => this.Context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => this.Context.Set<TEntity>().AddRange(entities);
        public void Remove(TEntity entity) => this.Context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => this.Context.Set<TEntity>().RemoveRange(entities);
    }
}
