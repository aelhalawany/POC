using Poc.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Poc.Core.Repository
{
    public interface IRepository<T>
        where T : BaseEntity
    {
      
        T Add(T entity);
        IEnumerable<T> Add(IEnumerable<T> entities);
        long Count();
        long Count(Expression<Func<T, bool>> whereExpression);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);
        bool Delete(T entity);
        void Delete(IEnumerable<T> entities);
        bool DeleteById(object id);        
        bool Exists(T entity);
        bool Exists(object id);
        Task<bool> ExistsAsync(T entity);
        Task<bool> ExistsAsync(object id);
        IQueryable<T> GetAll();
        IEnumerable<T> GetAll(TimeSpan cacheTime);
        Task<List<T>> GetAllAsync();
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetByQuery(Expression<Func<T, bool>> filter, TimeSpan cacheTime);
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
       
        IEnumerable<T> GetWithPagination(int pageIndex, int pageSize, out int totalCount,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> filter = null,
          string includeProperties = "");
       
        Task<List<T>> GetWithPaginationAsync(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
           Expression<Func<T, bool>> filter = null,
         string includeProperties = "");
        
        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "");

        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, IEnumerable<Expression<Func<T, bool>>> anyFilters,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "");
    }
}