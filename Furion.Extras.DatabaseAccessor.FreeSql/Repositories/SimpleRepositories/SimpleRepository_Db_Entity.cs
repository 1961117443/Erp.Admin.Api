using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public class SimpleRepository<TDb, TEntity> : SimpleRepository<TEntity>, ISimpleRepository<TDb, TEntity> where TDb : IDbLocator where TEntity : class, new()
    {
        public SimpleRepository(UnitOfWorkManager<TDb> uowm, IFreeSqlRepository freeSqlRepository) : base(uowm, freeSqlRepository)
        {
        }
    }
    public class SimpleRepository<TDb, TEntity, TKey> : BaseRepository<TEntity, TKey>,  ISimpleRepository<TDb, TEntity, TKey> where TDb : IDbLocator where TEntity : class, new()
    {
        private readonly ISimpleRepository<TDb, TEntity> simpleRepository;

        public SimpleRepository(UnitOfWorkManager<TDb> uow, ISimpleRepository<TDb, TEntity> simpleRepository) : base(uow.Orm, null, null)
        {
            uow.Binding(this);
            this.simpleRepository = simpleRepository;
        }

        public dynamic DynamicDbContext => simpleRepository.DynamicDbContext;

        public IAdo Ado => simpleRepository.Ado;

        public ISelect<TEntity> Entities => simpleRepository.Entities;

        public bool Any(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false)
        {
            return simpleRepository.Any(whereExpression, ignoreQueryFilters);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false)
        {
            return simpleRepository.AnyAsync(whereExpression, ignoreQueryFilters);
        }

        public ISelect<TEntity> AsQueryable()
        {
            return simpleRepository.AsQueryable();
        }

        public IFreeSqlRepository<TChangeEntity, long> Change<TChangeEntity>() where TChangeEntity : class, new()
        {
            return simpleRepository.Change<TChangeEntity>();
        }

        public IFreeSqlRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>() where TChangeEntity : class, new()
        {
            return simpleRepository.Change<TChangeEntity, TChangeKey>();
        }

        public long Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return simpleRepository.Count(whereExpression);
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return simpleRepository.CountAsync(whereExpression);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return simpleRepository.FirstOrDefaultAsync(whereExpression);
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="TDto"> </typeparam>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual TDto Get<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOne<TDto>();
        }
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="TDto"> </typeparam>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual Task<TDto> GetAsync<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOneAsync<TDto>();
        }


        public TEntity Get(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.Get(exp);
        }

        public TDto Get<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.Get<TDto>(exp);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.GetAsync(exp);
        }

        public Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.GetAsync<TDto>(exp);
        }

        public TEntity Single(dynamic id)
        {
            return simpleRepository.Single(id);
        }

        public Task<TEntity> SingleAsync(dynamic id)
        {
            return simpleRepository.SingleAsync(id);
        }

        public bool SoftDelete(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.SoftDelete(exp);
        }


        public Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp)
        {
            return simpleRepository.SoftDeleteAsync(exp);
        }


        public int Update(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return simpleRepository.Update(entity, ignoreNullValues, ignoreQueryFilters);
        }

        public Task<int> UpdateAsync(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return simpleRepository.UpdateAsync(entity, ignoreNullValues, ignoreQueryFilters);
        }

        public int UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return simpleRepository.UpdateExclude(entity, propertyNames, ignoreNullValues, ignoreQueryFilters);
        }

        public Task<int> UpdateExcludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return simpleRepository.UpdateExcludeAsync(entity, propertyNames, ignoreNullValues, ignoreQueryFilters);
        }

        public int UpdateInclude(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return simpleRepository.UpdateInclude(entity, columns, ignoreQueryFilters);
        }

        public Task<int> UpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return simpleRepository.UpdateIncludeAsync(entity, columns, ignoreQueryFilters);
        }

        public ISelect<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return simpleRepository.Where(condition, predicate, ignoreQueryFilters);
        }

        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return simpleRepository.Where(predicate, ignoreQueryFilters);
        }
        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete(TKey id)
        {
            UpdateDiy.SetDto(new { IsDeleted = true }).WhereDynamic(id).ExecuteAffrows();
            return true;
        }
        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> SoftDeleteAsync(TKey id)
        {
            await UpdateDiy.SetDto(new { IsDeleted = true }).WhereDynamic(id).ExecuteAffrowsAsync();
            return true;
        }
    }
}
