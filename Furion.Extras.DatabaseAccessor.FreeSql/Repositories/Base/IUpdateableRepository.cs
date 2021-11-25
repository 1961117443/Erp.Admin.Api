using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 新增操作仓储
    /// </summary>
    public interface IUpdateableRepository<TEntity>
    {

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
    }
}
