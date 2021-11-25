using FreeSql;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 非泛型 FreeSql 仓储
    /// </summary>
    public partial interface IFreeSqlRepository
    {
        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity"> 实体类型 </typeparam>
        /// <typeparam name="TKey"> 主键类型 </typeparam>
        /// <returns> 仓储 </returns>
        IFreeSqlRepository<TEntity, TKey> Change<TEntity, TKey>()
            where TEntity : class, new();
    }

    /// <summary>
    /// 默认的仓储接口，连接【DefaultConnection】的数据库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IFreeSqlRepository<TEntity> : IFreeSqlRepository<TEntity, long> where TEntity : class, new()
    {
    }

    /// <summary>
    /// FreeSql 仓储接口定义
    /// 默认的仓储接口，连接【DefaultConnection】的数据库
    /// </summary>
    /// <typeparam name="TEntity"> </typeparam>
    /// <typeparam name="TKey"> </typeparam>
    public partial interface IFreeSqlRepository<TEntity, TKey> : //IBaseRepository<TEntity, TKey>,
        IUpdateableRepository<TEntity>, IDeleteableRepository<TEntity,TKey>, IQueryableRepository<TEntity>,IInsertableRepository<TEntity>
        where TEntity : class, new()
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
    }
}