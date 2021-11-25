using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 新增操作仓储
    /// </summary>
    public interface IDeleteableRepository<TEntity>
    {
        #region Delete
        int Delete(IEnumerable<TEntity> entitys);
        int Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);

        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        int SoftDelete(Expression<Func<TEntity, bool>> exp);


        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> exp);

        #endregion Delete
    }

    public interface IDeleteableRepository<TEntity, TKey>: IDeleteableRepository<TEntity>
    {
        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SoftDelete(TKey id);

        /// <summary>
        /// 软删除 （IsDeleted = true）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(TKey id);

        int Delete(TKey id);
        Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
