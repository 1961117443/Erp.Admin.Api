using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSql
{

    /// <summary>
    /// 指定数据库创建仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbLocator">定位数据库</typeparam>
    public partial class DbRepository<TEntity, TKey, TDbLocator> : FreeSqlRepository<TEntity, TKey>, IDbRepository<TEntity, TKey>, IDbRepository<TEntity, TKey, TDbLocator> where TEntity : class, new() where TDbLocator : IDbLocator
    {
        public DbRepository(UnitOfWorkManager<TDbLocator> uowm, IFreeSqlRepository freeSqlRepository) : base(uowm, freeSqlRepository)
        {

        }
    }

    #region 工厂
    /// <summary>
    /// 用来判断实体是否有指定数据库
    /// </summary>
    public partial class DbRepositoryFactory : IDbRepositoryFactory
    {
        #region 内部缓存
        private static readonly object locker = new object();
        class Nest
        {
            internal static Dictionary<string, Type> Cache = new Dictionary<string, Type>();
        }
        #endregion
        private readonly IServiceProvider serviceProvider;

        public DbRepositoryFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IFreeSqlRepository<TEntity, long> CreateDbRepository<TEntity>() where TEntity : class, new()
        {
            return this.CreateDbRepository<TEntity, long>();
        }

        /// <summary>
        /// 反射获取 TEntity 是否有配置特性 DbLocatorAttribute
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public IFreeSqlRepository<TEntity, TKey> CreateDbRepository<TEntity, TKey>() where TEntity : class, new()
        {
            var type = GetRealGenericType(typeof(TEntity), typeof(TKey), GetDbLocator<TEntity>());
            return (IFreeSqlRepository<TEntity, TKey>)serviceProvider.GetService(type);
        }
        public IFreeSqlRepository<TEntity, TKey> CreateDbRepository<TEntity, TKey, TDbLocator>() where TEntity : class, new() where TDbLocator : IDbLocator
        {
            var type = GetRealGenericType(typeof(TEntity), typeof(TKey), typeof(TDbLocator));
            return (IFreeSqlRepository<TEntity, TKey>)serviceProvider.GetService(type);
        }

        private Type GetDbLocator<TEntity>()
        {
            return GetDbLocator(typeof(TEntity));
        }

        private Type GetDbLocator(Type entity)
        {
            Type locator = null;
            var attr = entity.GetCustomAttribute<DbLocatorAttribute>();
            if (attr != null)
            {
                locator = attr.GetDbLocator();
            }
            if (locator == null && entity.FullName.StartsWith("Furion.Extras.Admin.NET"))
            {
                locator = typeof(SysDbLocator);
            }
            return locator;
        }

        /// <summary>
        /// 获取真实的泛型仓储类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pKey"></param>
        /// <param name="dbLocator"></param>
        /// <returns></returns>
        private Type GetRealGenericType(Type entity, Type pKey = null, Type dbLocator = null)
        {
            string cacheKey = $"{entity.FullName}:{pKey?.FullName}:{dbLocator?.FullName}";
            if (!Nest.Cache.ContainsKey(cacheKey))
            {
                lock (locker)
                {
                    if (!Nest.Cache.ContainsKey(cacheKey))
                    {
                        if (pKey == null && dbLocator == null)
                        {
                            Nest.Cache.Add(cacheKey, typeof(IFreeSqlRepository<>).MakeGenericType(entity));
                        }
                        else if (pKey != null && dbLocator != null)
                        {
                            var iface = typeof(IDbRepository<,,>).MakeGenericType(entity, pKey, dbLocator);
                            Nest.Cache.Add(cacheKey, iface);
                        }
                        else if (dbLocator != null)
                        {
                            var iface = typeof(IDbRepository<,,>).MakeGenericType(entity, typeof(long), dbLocator);
                            Nest.Cache.Add(cacheKey, iface);
                        }
                        else if(pKey != null)
                        {
                            Nest.Cache.Add(cacheKey, typeof(IFreeSqlRepository<,>).MakeGenericType(entity, pKey));
                        }
                        else
                        {
                            Nest.Cache.Add(cacheKey, typeof(IFreeSqlRepository<,>).MakeGenericType(entity, typeof(long)));
                        }
                    }
                }
            }
            return Nest.Cache[cacheKey];
        }

        public IDbRepository<TEntity, TKey> CreateDbRepositoryV2<TEntity, TKey, TDbLocator>()
            where TEntity : class, new()
            where TDbLocator : IDbLocator
        {
            var type = GetRealGenericType(typeof(TEntity), typeof(TKey), typeof(TDbLocator));
            return (IDbRepository<TEntity, TKey>)serviceProvider.GetService(type);
        }

        public IDbRepository<TEntity, long> CreateDbRepositoryV2<TEntity>() where TEntity : class, new()
        {
            var locator = GetDbLocator<TEntity>();
            var type = GetRealGenericType(typeof(TEntity), null, locator);
            return this.CreateDbRepositoryV2<TEntity, long>();
        }

        public IDbRepository<TEntity, TKey> CreateDbRepositoryV2<TEntity, TKey>() where TEntity : class, new()
        {
            var type = GetRealGenericType(typeof(TEntity), typeof(TKey), GetDbLocator<TEntity>());
            return (IDbRepository<TEntity, TKey>)serviceProvider.GetService(type);
        }
    }
    #endregion
}
