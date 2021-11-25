using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public interface ISimpleRepository<TDb,TEntity> : IFreeSqlRepository<TEntity> where TDb : IDbLocator where TEntity : class, new()
    {
    }
    public interface ISimpleRepository<TDb, TEntity,TKey> : IFreeSqlRepository<TEntity, TKey> where TDb : IDbLocator where TEntity : class, new()
    {
        ///// <summary>
        ///// 软删除 （IsDeleted = true）
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //bool SoftDelete(TKey id);
        ///// <summary>
        ///// 软删除 （IsDeleted = true）
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> SoftDeleteAsync(TKey id);
    }
}
