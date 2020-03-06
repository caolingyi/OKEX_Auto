using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using OKEX.Auto.Core.Context;
using OKEX.Auto.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ORM.BaseEFRepository
{
    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly DefaultEFDBContext _context;
        private DbSet<TEntity> _entities;

        #endregion

        #region Ctor

        public EFRepository(DefaultEFDBContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            _context.SaveChanges();
            return exception.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetById(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.Init();
                Entities.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                foreach (var item in entities)
                {
                    item.Init();
                }
                Entities.AddRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual async Task EnableAsync(long id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.Enable();
                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual async Task DisableAsync(long id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.Disable();
                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }


        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> GetAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> keySelector, bool isAscending)
        {
            if (isAscending)
            {
                return await Entities.OrderBy(keySelector).FirstOrDefaultAsync(expression);
            }
            else
            {
                return await Entities.OrderByDescending(keySelector).FirstOrDefaultAsync(expression);
            }
        }

        public virtual async Task<IList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.Where(expression).ToListAsync();
            //.AsExpandable()
        }

        public virtual async Task<IList<TEntity>> QueryAsync<TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TKey>> keySelector, bool isAscending, PageParam pageParam)
        {
            pageParam.TotalCount = 0;
            var query = Table.Where(expression);
            //.AsExpandable()
            if (isAscending)
            {
                query = query.OrderBy(keySelector);
            }
            else
            {
                query = query.OrderByDescending(keySelector);
            }

            if (pageParam.PageSize > 0) //分页查询
            {
                if (pageParam.PageIndex < 0)
                {
                    pageParam.PageIndex = 0;
                }
                pageParam.TotalCount = query.Count();
                query = query.Skip(pageParam.PageIndex).Take(pageParam.PageSize);
            }

            return await query.ToListAsync();
        }

        #endregion

        #region BulkExtensions应用

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="bulkInsert"></param>
        public virtual async Task BatchAddBulkInsert(List<TEntity> bulkInsert)
        {
            await _context.BulkInsertAsync(bulkInsert);
        }
        /// <summary>
        /// 批量更新数据(更新所有字段)
        /// </summary>
        /// <param name="conditionExpression"></param>
        /// <param name="updateExpression"></param>
        public virtual async Task BatchUpdateBulkInsertTest(Expression<Func<TEntity, bool>> conditionExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            await _context.Set<TEntity>().Where(conditionExpression).BatchUpdateAsync(updateExpression);
        }
        ///// <summary>
        ///// 批量更新数据(更新指定字段)
        ///// </summary>
        ///// <param name="conditionExpression"></param>
        ///// <param name="updateValue"></param>
        ///// <param name="updateColumns"></param>
        //public void BatchUpdateBulkInsertTest(Expression<Func<TEntity, bool>> conditionExpression, TEntity updateValue, List<string> updateColumns = null)
        //{
        //    _context.Set<TEntity>().Where(conditionExpression).BatchUpdate(updateValue, updateColumns);
        //}

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="conditionExpression"></param>
        public virtual async Task BatchDeleteBulkInsertTest(Expression<Func<TEntity, bool>> conditionExpression)
        {
            await _context.Set<TEntity>().Where(conditionExpression).BatchDeleteAsync();
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        #endregion
    }
}
