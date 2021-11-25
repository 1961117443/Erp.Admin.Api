using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public interface ISimpleRepository<TEntity> where TEntity:class,new()
    {

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        dynamic DynamicDbContext { get; }

        /// <summary>
        /// 原生 Ado 对象
        /// </summary>
        IAdo Ado { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        ISelect<TEntity> Entities { get; }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity"> 实体类型 </typeparam>
        /// <returns> 仓储 </returns>
        IFreeSqlRepository<TChangeEntity, long> Change<TChangeEntity>()
                where TChangeEntity : class, new();

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity"> 实体类型 </typeparam>
        /// <typeparam name="TChangeKey"> 主键类型 </typeparam>
        /// <returns> 仓储 </returns>
        IFreeSqlRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>()
                where TChangeEntity : class, new();

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="whereExpression"> </param>
        /// <returns> </returns>
        long Count(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="whereExpression"> </param>
        /// <returns> </returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="whereExpression"> </param>
        /// <returns> </returns>
        bool Any(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false);

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="whereExpression"> </param>
        /// <returns> </returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression, bool ignoreQueryFilters = false);

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        TEntity Single(dynamic id);

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"> </param>
        /// <returns> </returns>
        Task<TEntity> SingleAsync(dynamic id);

        /// <summary>
        /// </summary>
        /// <param name="condition"> </param>
        /// <param name="predicate"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        ISelect<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false);
        /// <summary>
        /// </summary>
        /// <param name="predicate"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        ISelect<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool ignoreQueryFilters = false);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <returns> </returns>
        ISelect<TEntity> AsQueryable();

        /// <summary>
        /// </summary>
        /// <param name="whereExpression"> </param>
        /// <returns> </returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);

        #region Select

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        TDto Get<TDto>(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        ///
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp);

        #endregion Select

        #region Update

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        int Update(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="columns">属性表达式</param>
        /// <param name="ignoreQueryFilters">是否忽略过滤器</param>
        /// <returns></returns>
        int UpdateInclude(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false);

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        Task<int> UpdateAsync(TEntity entity, bool? ignoreNullValues = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="columns">属性表达式</param>
        /// <param name="ignoreQueryFilters">是否忽略过滤器</param>
        /// <returns></returns>
        Task<int> UpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>> columns, bool ignoreQueryFilters = false);

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="propertyNames"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>
        int UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="propertyNames"> </param>
        /// <param name="ignoreNullValues"> </param>
        /// <param name="ignoreQueryFilters"> </param>
        /// <returns> </returns>

        Task<int> UpdateExcludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, bool ignoreQueryFilters = false);

        #endregion Update

        #region Delete

        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        bool SoftDelete(Expression<Func<TEntity, bool>> exp);


        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp);

        #endregion Delete
    }
    public interface ISimpleRepository<TDb,TEntity> : ISimpleRepository<TEntity> where TDb : IDbLocator where TEntity : class, new()
    {
    }
    public interface ISimpleRepository<TDb, TEntity,TKey> : ISimpleRepository<TDb, TEntity> where TDb : IDbLocator where TEntity : class, new()
    {
        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftDelete(TKey id);
        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(TKey id);
    }

    public interface ISimpleRepositoryEx<TEntity, TKey> : ISimpleRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftDelete(TKey id);
        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(TKey id);
    }
}
