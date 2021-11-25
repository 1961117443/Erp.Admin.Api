using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 新增操作仓储
    /// </summary>
    public interface IQueryableRepository<TEntity>
    {
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
    }
}
