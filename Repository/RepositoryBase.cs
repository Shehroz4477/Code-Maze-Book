using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext Context;
        protected RepositoryBase(RepositoryContext repositoryContext) => Context = repositoryContext;
        public void Create(T entity) => Context.Set<T>().Add(entity);
        public void Update(T entity) => Context.Set<T>().Update(entity);
        public void Delete(T entity) => Context.Set<T>().Remove(entity);
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges
            ? Context.Set<T>().Where(expression).AsNoTracking()
            : Context.Set<T>().Where(expression);
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges
            ? Context.Set<T>().AsNoTracking()
            : Context.Set<T>();
    }
}
