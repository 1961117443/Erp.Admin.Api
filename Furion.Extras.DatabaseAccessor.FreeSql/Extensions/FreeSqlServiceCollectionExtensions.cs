using FreeSql;
using FreeSql.Aop;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// FreeSql 服务拓展类
    /// </summary>
    public static class FreeSqlServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 FreeSql 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="dataType"></param>
        /// <param name="buildAction"></param>
        /// <param name="freeSqlAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql(this IServiceCollection services, string connectionString, DataType dataType, Action<FreeSqlBuilder> buildAction = default, Action<IFreeSql> freeSqlAction = default)
        {
            return services.AddFreeSql(freeSqlBuilder =>
            {
                freeSqlBuilder.UseConnectionString(dataType, connectionString);
                buildAction?.Invoke(freeSqlBuilder);
            }, freeSqlAction);
        }
        /// <summary>
        /// 添加 FreeSql 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="dataType"></param>
        /// <param name="buildAction"></param>
        /// <param name="freeSqlAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql<TMark>(this IServiceCollection services, string connectionString, DataType dataType, Action<FreeSqlBuilder> buildAction = default, Action<IFreeSql> freeSqlAction = default)
        {
            return services.AddFreeSql<TMark>(freeSqlBuilder =>
            {
                freeSqlBuilder.UseConnectionString(dataType, connectionString);
                buildAction?.Invoke(freeSqlBuilder);
            }, freeSqlAction);
        }
        /// <summary>
        /// 添加 FreeSql 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buildAction"></param>
        /// <param name="freeSqlAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql(this IServiceCollection services, Action<FreeSqlBuilder> buildAction = default, Action<IFreeSql> freeSqlAction = default)
        {
            services.AddSingleton<IFreeSql>(u =>
            {
                //创建builder
                var freeSqlBuilder = new FreeSqlBuilder();
                freeSqlBuilder.UseAutoSyncStructure(false);

                buildAction?.Invoke(freeSqlBuilder);

                //创建实例
                var freeSql = freeSqlBuilder.Build();
                //aop拦截
                freeSql.Aop.ConfigEntityProperty -= ConfigEntityProperty;
                freeSql.Aop.ConfigEntityProperty += ConfigEntityProperty;
                freeSql.Aop.CurdBefore += CurdBefore;

                freeSqlAction?.Invoke(freeSql);

                return freeSql;
            });

            return services;
        }

        /// <summary>
        /// 添加 FreeSql<> 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buildAction"></param>
        /// <param name="freeSqlAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql<TMark>(this IServiceCollection services, Action<FreeSqlBuilder> buildAction = default, Action<IFreeSql> freeSqlAction = default)
        {
            services.AddSingleton<IFreeSql<TMark>>(u =>
            {
                //创建builder
                var freeSqlBuilder = new FreeSqlBuilder();
                freeSqlBuilder.UseAutoSyncStructure(false);

                buildAction?.Invoke(freeSqlBuilder);

                //创建实例
                var freeSql = freeSqlBuilder.Build<TMark>();

                //aop拦截
                freeSql.Aop.ConfigEntityProperty -= ConfigEntityProperty;
                freeSql.Aop.ConfigEntityProperty += ConfigEntityProperty;
                freeSql.Aop.CurdBefore += CurdBefore;

                freeSqlAction?.Invoke(freeSql);


                return freeSql;
            });

            return services;
        }
        /// <summary>
        ///  注册 FreeSql 仓储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buildAction"></param>
        /// <param name="freeSqlAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSqlRepository(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWorkManager>();
            services.AddScoped(typeof(UnitOfWorkManager<>));

            // 注册非泛型仓储
            services.AddScoped<IFreeSqlRepository, FreeSqlRepository>();

            // 注册 FreeSql 仓储
            services.AddScoped(typeof(IFreeSqlRepository<>), typeof(FreeSqlRepository<>));
            services.AddScoped(typeof(IFreeSqlRepository<,>), typeof(FreeSqlRepository<,>));
            services.AddScoped(typeof(ISimpleRepository<,>), typeof(SimpleRepository<,>));
            services.AddScoped(typeof(ISimpleRepository<,,>), typeof(SimpleRepository<,,>));

            //services.AddScoped(typeof(IFreeSqlRepository<>), typeof(DbRepository<>));
            //services.AddScoped(typeof(IFreeSqlRepository<,>), typeof(DbRepository<,>));

            //services.AddScoped<IDbRepositoryFactory, DbRepositoryFactory>();
            //services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
            //services.AddScoped(typeof(IDbRepository<,>), typeof(DbRepository<,>));
            //services.AddScoped(typeof(IDbRepository<,,>), typeof(DbRepository<,,>));

            //services.AddScoped(typeof(ISimpleRepository<>), typeof(SimpleRepository<>));
            //services.AddScoped(typeof(ISimpleRepository<,>), typeof(SimpleRepository<,>));
            //services.AddScoped(typeof(ISimpleRepository<,,>), typeof(SimpleRepository<,,>));
            //services.AddScoped(typeof(ISimpleRepositoryEx<,>), typeof(SimpleRepositoryEx<,>));


            return services;
        }

        /// <summary>
        /// aop拦截监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConfigEntityProperty(object sender, ConfigEntityPropertyEventArgs e)
        {
            //枚举类型全局映射为int
            if (e.Property.PropertyType.IsEnum)
            {
                e.ModifyResult.MapType = typeof(int);
            }
        }

        private static void CurdBefore(object sender, CurdBeforeEventArgs e)
        {
             
        }
    }
}
