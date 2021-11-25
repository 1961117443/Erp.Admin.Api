using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 新增操作仓储
    /// </summary>
    public interface IInsertableRepository<TEntity>
    {
        List<TEntity> Insert(IEnumerable<TEntity> entitys);
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default);
    }
}
