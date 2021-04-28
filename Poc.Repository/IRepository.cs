using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Poc.Repository
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        ///     Gets the context.
        /// </summary>
        //DbContext Context { get; }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Add(T entity);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> Add(IEnumerable<T> entities);

       

        /// <summary>
        ///     The count.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        long Count();

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        long Count(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Delete(T entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Delete(IEnumerable<T> entities);

       

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DeleteById(object id);        

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(T entity);

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(object id);

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsAsync(T entity);

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsAsync(object id);

        /// <summary>
        ///     The get all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> GetAll(TimeSpan cacheTime);

        /// <summary>
        ///     The get all async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// The get by query.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter);

        /// <summary>
        /// The get by query.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> GetByQuery(Expression<Func<T, bool>> filter, TimeSpan cacheTime);

        /// <summary>
        /// Used to execute raw sql queries to used DbSet 
        /// </summary>
        /// <param name="query">Query to be executed</param>
        /// <param name="parameters">Parameters to be passed to query</param>
        /// <returns></returns>
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);


        /// <summary>
        /// Get paged result with option include, filter and order 
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties"></param>
        /// <param name="includeProperties">Included Properties</param>
        IEnumerable<T> GetWithPagination(int pageIndex, int pageSize, out int totalCount,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> filter = null,
          string includeProperties = "");

        /// <summary>
        /// Get paged reslut with option include, filter and order async
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties">included Properties</param>
        /// <returns></returns>
        Task<List<T>> GetWithPaginationAsync(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
           Expression<Func<T, bool>> filter = null,
         string includeProperties = "");

        /// <summary>
        /// Get items with paging and dynamic filters
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filters">Filters</param>
        /// <param name="includeProperties">included Properties</param>
        /// <returns>List of items</returns>
        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "");

        /// <summary>
        /// Get items with paging and dynamic filters
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="anyFilters">Any Filters</param>
        /// <param name="filters">Filters</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, IEnumerable<Expression<Func<T, bool>>> anyFilters,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "");
    }
}