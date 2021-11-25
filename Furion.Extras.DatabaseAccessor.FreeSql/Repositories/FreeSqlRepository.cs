using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 非泛型 FreeSql 仓储
    /// </summary>
    public partial class FreeSqlRepository : IFreeSqlRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"> 服务提供器 </param>
        public FreeSqlRepository(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity"> 实体类型 </typeparam>
        /// <typeparam name="TKey"> 主键类型 </typeparam>
        /// <returns> 仓储 </returns>
        public virtual IFreeSqlRepository<TEntity, TKey> Change<TEntity, TKey>()
            where TEntity : class, new()
        {
            return _serviceProvider.GetService<IFreeSqlRepository<TEntity, TKey>>();
        }
    }

    /// <summary>
    /// FreeSql 泛型仓储
    /// </summary>
    /// <typeparam name="TEntity"> </typeparam>
    public partial class FreeSqlRepository<TEntity> : FreeSqlRepository<TEntity, long>, IFreeSqlRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uowm"> </param>
        /// <param name="freeSqlRepository"> </param>
        public FreeSqlRepository(UnitOfWorkManager uowm, IFreeSqlRepository freeSqlRepository) : base(uowm, freeSqlRepository)
        {
        }
    }

    /// <summary>
    /// FreeSql 泛型仓储
    /// </summary>
    /// <typeparam name="TEntity"> </typeparam>
    /// <typeparam name="TKey"> </typeparam>
    public partial class FreeSqlRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>, IFreeSqlRepository<TEntity, TKey> where TEntity : class, new()
    {
        /// <summary>
        /// 非泛型 FreeSql 仓储
        /// </summary>
        private readonly IFreeSqlRepository _freeSqlRepository;

        /// <summary>
        /// 初始化 SqlSugar 客户端
        /// </summary>
        private readonly IFreeSql _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uowm"> 工作单元 </param>
        /// <param name="freeSqlRepository"> 非泛型 FreeSql 仓储 </param>
        public FreeSqlRepository(UnitOfWorkManager uowm, IFreeSqlRepository freeSqlRepository) : base(uowm.Orm, null, null)
        {
            uowm.Binding(this);
            _freeSqlRepository = freeSqlRepository;
            DynamicDbContext = Context = _db = uowm.Orm;
            Ado = _db.Ado;
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        public virtual ISelect<TEntity> Entities => Orm.Queryable<TEntity>();

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual IFreeSql Context { get; }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        public virtual dynamic DynamicDbContext { get; }

        /// <summary>
        /// 原生 Ado 对象
        /// </summary>
        public virtual IAdo Ado { get; }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity"> 实体类型 </typeparam>
        /// <returns> 仓储 </returns>
        public virtual IFreeSqlRepository<TChangeEntity, long> Change<TChangeEntity>()
            where TChangeEntity : class, new()
        {
            return _freeSqlRepository.Change<TChangeEntity, long>();
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity"> 实体类型 </typeparam>
        /// <typeparam name="TChangeKey"> 主键类型 </typeparam>
        /// <returns> 仓储 </returns>
        public virtual IFreeSqlRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>()
            where TChangeEntity : class, new()
        {
            return _freeSqlRepository.Change<TChangeEntity, TChangeKey>();
        }

        #region Select

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
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOne();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual TDto Get<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOne<TDto>();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="expression"> 条件 </param>
        /// <returns> </returns>
        public virtual long Count(Expression<Func<TEntity, bool>> expression)
        {
            Select.Where(expression).Count(out long total);
            return total;
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="expression"> </param>
        /// <returns> </returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression, bool ignoreQueryFilters = false)
        {
            var entities = Select;
            if (ignoreQueryFilters)
            {
                entities = Select.DisableGlobalFilter();
            }
            return entities.Any(expression);
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual TEntity Single(dynamic id)
        {
            return Select.WhereDynamic(id).ToOne();
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync<TDto>();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="expression"> 条件 </param>
        /// <returns> </returns>
        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Select.Where(expression).CountAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false)
        {
            var entities = Select;
            if (ignoreQueryFilters)
            {
                entities = Select.DisableGlobalFilter();
            }
            return await entities.AnyAsync(whereExpression);
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        public virtual async Task<TEntity> SingleAsync(dynamic id)
        {
            return await Select.WhereDynamic(id).ToOneAsync();
        }

        #endregion Select

        #region Update

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        public virtual int Update(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            var isIgnore = ignoreNullValues ?? false;
            if (isIgnore)
            {
                return (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSourceIgnore(entity, GetIgnoreFunc()).ExecuteAffrows();
            }
            else
            {
                return (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).ExecuteAffrows();
            }
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="columns">属性表达式</param>
        /// <param name="ignoreQueryFilters">是否忽略过滤器</param>
        /// <returns></returns>
        public virtual int UpdateInclude(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).UpdateColumns(columns).ExecuteAffrows();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="propertyNames"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        public int UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).IgnoreColumns(IgnoreNullValues(entity, propertyNames, GetIgnoreFunc(), ignoreNullValues)).ExecuteAffrows();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        public virtual async Task<int> UpdateAsync(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            var isIgnore = ignoreNullValues ?? false;
            if (isIgnore)
            {
                return await (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSourceIgnore(entity, GetIgnoreFunc()).ExecuteAffrowsAsync();
            }
            else
            {
                return await (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).ExecuteAffrowsAsync();
            }
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="columns">属性表达式</param>
        /// <param name="ignoreQueryFilters">是否忽略过滤器</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false)
        {
            return await (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).UpdateColumns(columns).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="propertyNames"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        public async Task<int> UpdateExcludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false)
        {
            return await (ignoreQueryFilters ? UpdateDiy.DisableGlobalFilter() : UpdateDiy).SetSource(entity).IgnoreColumns(IgnoreNullValues(entity, propertyNames, GetIgnoreFunc(), ignoreNullValues)).ExecuteAffrowsAsync();
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SoftDelete(TKey id)
        {
            return UpdateDiy.SetDto(new { IsDeleted = true }).WhereDynamic(id).ExecuteAffrows();
        }

        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int SoftDelete(Expression<Func<TEntity, bool>> exp)
        {
            return UpdateDiy.SetDto(new { IsDeleted = true }).Where(exp).ExecuteAffrows();
        }

        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(TKey id)
        {
            return await UpdateDiy.SetDto(new { IsDeleted = true }).WhereDynamic(id).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 软删除 （IsDeleted）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await UpdateDiy.SetDto(new { IsDeleted = true }).Where(exp).ExecuteAffrowsAsync();
        }

        #endregion Delete

        #region ReadableRepository

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public virtual ISelect<TEntity> AsQueryable()
        {
            return Select;
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        public virtual ISelect<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var entities = Select;
            if (ignoreQueryFilters)
            {
                entities = Select.DisableGlobalFilter();
            }
            if (predicate != null) entities = entities.Where(predicate);
            return entities;
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="condition"> 条件 </param>
        /// <param name="predicate"> 表达式 </param>
        /// <param name="ignoreQueryFilters"> 是否忽略查询过滤器 </param>
        /// <returns> 数据库中的多个实体 </returns>
        public virtual ISelect<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var entities = Select;
            if (ignoreQueryFilters)
            {
                entities = Select.DisableGlobalFilter();
            }
            return entities.WhereIf(condition, predicate);
        }
        public ISelect<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false)
        {
            var entities = Select;
            if (ignoreQueryFilters)
            {
                entities = Select.DisableGlobalFilter();
            }
            return entities.Where(predicate);
        }
        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Select.Where(whereExpression).FirstAsync();
        }

        #endregion ReadableRepository

        private static Func<object, bool> GetIgnoreFunc()
        {
            return col => col == null || string.IsNullOrEmpty(col?.ToString()) || col?.ToString() == new DateTime().ToString() || col?.ToString() == new DateTimeOffset().ToString() || col?.ToString() == Guid.Empty.ToString();
        }

        /// <summary>
        /// 忽略空值属性
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="excludeProperty"> </param>
        /// <param name="ignore"> </param>
        /// <param name="ignoreNullValues"> </param>
        private string[] IgnoreNullValues(TEntity entity, string[] excludeProperty, Func<object, bool> ignore, bool? ignoreNullValues = null)
        {
            var result = new List<string>();
            if (excludeProperty != null)
                result.AddRange(excludeProperty.ToList());
            if (ignore == null) return result.ToArray();

            var isIgnore = ignoreNullValues ?? false;
            if (isIgnore == false) return result.ToArray();

            // 获取所有的属性
            var properties = EntityType?.GetProperties();
            if (properties == null) return result.ToArray();

            result.AddRange(properties.Select(prop => new { Prop = prop, Val = prop.GetValue(entity) })
                .Where(a => ignore(a.Val))
                .Select(val => val.Prop.Name));

            return result.ToArray();
        }

        
    }
}