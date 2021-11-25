using System;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSql
{
    /// <summary>
    /// 可以指定数据库的仓储
    /// </summary>
    /// <typeparam name="TDb">数据库</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class SimpleRepository<TDb, TEntity> : FreeSqlRepository<TEntity>, ISimpleRepository<TDb, TEntity>, IFreeSqlRepository<TEntity> where TDb : IDbLocator where TEntity : class, new()
    {
        public SimpleRepository(UnitOfWorkManager<TDb> uowm, IFreeSqlRepository freeSqlRepository) : base(uowm, freeSqlRepository)
        {
        }
    }
    public class SimpleRepository<TDb, TEntity, TKey> : FreeSqlRepository<TEntity, TKey>, ISimpleRepository<TDb, TEntity, TKey>, IFreeSqlRepository<TEntity, TKey> where TDb : IDbLocator where TEntity : class, new()
    {
        private readonly IServiceProvider serviceProvider;

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
        public SimpleRepository(UnitOfWorkManager<TDb> uowm, IFreeSqlRepository freeSqlRepository,IServiceProvider serviceProvider) : base(uowm, freeSqlRepository)
        {
            this.serviceProvider = serviceProvider;
        }

        public override IFreeSqlRepository<TChangeEntity, TChangeKey> Change<TChangeEntity, TChangeKey>()
        {
            return serviceProvider.GetService<ISimpleRepository<TDb, TChangeEntity, TChangeKey>>();
        }

        public override IFreeSqlRepository<TChangeEntity, long> Change<TChangeEntity>()
        {
            return this.Change<TChangeEntity,long>();
        }
    }
}
