using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 通过工厂创建仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDbRepositoryFactory
    {
        IFreeSqlRepository<TEntity,long> CreateDbRepository<TEntity>() where TEntity : class, new();
        IFreeSqlRepository<TEntity, TKey> CreateDbRepository<TEntity, TKey>() where TEntity : class, new();
        IFreeSqlRepository<TEntity, TKey> CreateDbRepository<TEntity, TKey, TDbLocator>() where TEntity : class, new() where TDbLocator : IDbLocator;
        IDbRepository<TEntity, long> CreateDbRepositoryV2<TEntity>() where TEntity : class, new();
        IDbRepository<TEntity, TKey> CreateDbRepositoryV2<TEntity, TKey>() where TEntity : class, new();
        IDbRepository<TEntity, TKey> CreateDbRepositoryV2<TEntity, TKey, TDbLocator>() where TEntity : class, new() where TDbLocator : IDbLocator;
    }
    //public partial interface IDbRepositoryFactory<TDbLocator> where TDbLocator : IDbLocator
    //{
    //    IFreeSqlRepository<TEntity, long> CreateDbRepository<TEntity>() where TEntity : class, new();
    //    IFreeSqlRepository<TEntity, TKey> CreateDbRepository<TEntity, TKey>() where TEntity : class, new();
    //    IDbRepository<TEntity, TKey, TDbLocator> CreateDbRepositoryV2<TEntity, TKey>() where TEntity : class, new() where TDbLocator : IDbLocator;
    //}

    /// <summary>
    /// 适配 IFreeSqlRepository<TEntity>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDbRepository<TEntity> : IFreeSqlRepository<TEntity> where TEntity : class, new()
    {
    }

    /// <summary>
    /// 适配 IFreeSqlRepository<TEntity, TKey> 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDbRepository<TEntity, TKey> :IFreeSqlRepository<TEntity, TKey> where TEntity : class, new()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDbRepository<TEntity, TKey, TDbLocator> : IFreeSqlRepository<TEntity, TKey> where TEntity : class, new()
    {
    }

}
