using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Poc.Repository
{
   
    public  class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BaseRepository(DbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        private DbContext Context { get; }

        /// <summary>
        ///     Gets the db set.
        /// </summary>
        private DbSet<T> DbSet { get; }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Add(T entity)
        {
            var addedEntity = this.DbSet.Add(entity);

            if (this.Context.Entry(addedEntity).State == EntityState.Added)
            {
                return addedEntity;
            }

            return null;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> Add(IEnumerable<T> entities)
        {
            var added = this.DbSet.AddRange(entities);
            return added;
        }

       

        /// <summary>
        ///     The count.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        public virtual long Count()
        {
            return this.DbSet.Count();
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public virtual long Count(Expression<Func<T, bool>> whereExpression)
        {
            return this.DbSet.Count(whereExpression);
        }

        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual async Task<int> CountAsync()
        {
            return await this.Context.Set<T>().CountAsync();
        }

        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await this.Context.Set<T>().CountAsync(whereExpression);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Delete(T entity)
        {
            //DbSet.Attach(entity);
            var deletedEntity = this.DbSet.Remove(entity);
            return this.Context.Entry(deletedEntity).State == EntityState.Deleted;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        

       

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool DeleteById(object id)
        {
            var entity = this.GetById(id);
            return this.Delete(entity);
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(T entity)
        {
            return this.DbSet.Any(e => e == entity);
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(object id)
        {
            return this.DbSet.Find(id) != null;
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(T entity)
        {
            return await this.DbSet.AnyAsync(e => e == entity);
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(object id)
        {
            return await this.DbSet.FindAsync(id) != null;
        }

        /// <summary>
        ///     The get all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public virtual IQueryable<T> GetAll()
        {
            return this.DbSet.Select(e => e);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> GetAll(TimeSpan cacheTime)
        {
            return this.DbSet.Select(e => e); // .FromCache(CachePolicy.WithDurationExpiration(cacheTime));
        }

        /// <summary>
        ///     The get all async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual Task<List<T>> GetAllAsync()
        {
            return this.DbSet.Select(e => e).ToListAsync();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<T> GetByIdAsync(object id)
        {
            return this.DbSet.FindAsync(id);
        }

        /// <summary>
        /// The get by query.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public virtual IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter)
        {
            return this.DbSet.Where(filter);
        }

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
        public virtual IEnumerable<T> GetByQuery(Expression<Func<T, bool>> filter, TimeSpan cacheTime)
        {
            return this.DbSet.Where(filter);

            // .FromCache(CachePolicy.WithDurationExpiration(cacheTime));
        }

        /// <summary>
        /// The insert or update.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        public virtual void InsertOrUpdate(T t, Expression<Func<T, bool>> predicate)
        {
            var exists = this.DbSet.Where(predicate).Any();
            if (exists)
            {
                this.Update(t);
            }
            else
            {
                this.Add(t);
            }
        }

        /// <summary>
        ///     The save async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        //public virtual async Task<bool> SaveAsync()
        //{
        //    if (this.Context.IsDirty())
        //    {
        //        return await this.Context.SaveChangesAsync() > 0;
        //    }

        //    return true;
        //}

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        public virtual void Update(T t)
        {
            DbEntityEntry dbEntityEntry = this.Context.Entry(t);

            this.Context.Set<T>().Attach(t);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="t">
        /// The t.
        /// </param>
        public virtual void Update(object id, T t)
        {
            var obj = this.GetById(id);
            this.Context.Entry(obj).CurrentValues.SetValues(t);
        }

        /// <summary>
        /// Used to execute raw sql queries to used DbSet 
        /// </summary>
        /// <param name="query">Query to be executed</param>
        /// <param name="parameters">Parameters to be passed to query</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).ToList();
        }

        /// <summary>
        /// Get paged result with option include, filter and order 
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetWithPagination(int pageIndex, int pageSize, out int totalCount,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> filter = null,
          string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }

        /// <summary>
        /// Get paged reslut with option include, filter and order async
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        public virtual Task<List<T>> GetWithPaginationAsync(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
           Expression<Func<T, bool>> filter = null,
         string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToListAsync();
        }

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
        public virtual IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }

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
        public virtual IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, IEnumerable<Expression<Func<T, bool>>> anyFilters,
          IEnumerable<Expression<Func<T, bool>>> filters = null,
         string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();            

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (anyFilters != null && anyFilters.Count() > 0)
            {
                query = query.Where(anyFilters.FirstOrDefault());
                foreach (var anyfilter in anyFilters)
                {
                    query = query.Union(query.Where(anyfilter));
                }                
            }
            
            totalCount = query.Count();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }        
    }
}