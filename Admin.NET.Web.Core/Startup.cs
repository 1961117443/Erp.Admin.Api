using Furion.Extras.Admin.NET;
using Furion.Extras.Admin.NET.Service;
using Furion;
using Furion.Extras.Admin.NET.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnceMi.AspNetCore.OSS;
using Serilog;
using Yitter.IdGenerator;
using FreeSql;
using System;
using Furion.DatabaseAccessor;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Furion.JsonSerialization;

namespace Admin.NET.Web.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            FreeSqlConfigure(services);

            services.AddConfigurableOptions<RefreshTokenSettingOptions>();
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            services.AddCorsAccessor();
            services.AddRemoteRequest();
            services.AddControllersWithViews()
                    .AddMvcFilter<RequestActionFilter>()                    
                    .AddNewtonsoftJson(options =>
                    {
                        // 首字母小写(驼峰样式)
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        // 时间格式化
                        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                        // 忽略循环引用
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        // 忽略空值
                        // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    })
                    .AddInjectWithUnifyResult<XnRestfulResultProvider>();
            services.AddViewEngine();
            services.AddSignalR();
            services.AddSimpleEventBus();

            if (App.Configuration["Cache:CacheType"] == "RedisCache")
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = App.Configuration["Cache:RedisConnectionString"]; // redis连接配置
                    options.InstanceName = App.Configuration["Cache:InstanceName"]; // 键名前缀
                });
            }

            //// default minio
            //// 添加默认对象储存配置信息
            //services.AddOSSService(option =>
            //{
            //    option.Provider = OSSProvider.Minio;
            //    option.Endpoint = "oss.oncemi.com:9000";
            //    option.AccessKey = "Q*************9";
            //    option.SecretKey = "A**************************Q";
            //    option.IsEnableHttps = true;
            //    option.IsEnableCache = true;
            //});

            // aliyun oss
            // 添加名称为‘aliyunoss’的OSS对象储存配置信息
            services.AddOSSService("aliyunoss", option =>
            {
                option.Provider = OSSProvider.Aliyun;
                option.Endpoint = "oss-cn-hangzhou.aliyuncs.com";
                option.AccessKey = "L*******************U";
                option.SecretKey = "5*******************************T";
                option.IsEnableCache = true;
            });

            //// qcloud oss
            //// 从配置文件中加载节点为‘OSSProvider’的配置信息
            //services.AddOSSService("QCloud", "OSSProvider");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //  NGINX 反向代理获取真实IP
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // 添加状态码拦截中间件
            app.UseUnifyResultStatusCodes();

            app.UseHttpsRedirection(); // 强制https
            app.UseStaticFiles();

            // Serilog请求日志中间件---必须在 UseStaticFiles 和 UseRouting 之间
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCorsAccessor();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseInject(string.Empty);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/hubs/chathub");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // 设置雪花Id的workerId，确保每个实例workerId都应不同
            var workerId = ushort.Parse(App.Configuration["SnowId:WorkerId"] ?? "1");
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = workerId });

            // 开启自启动定时任务
            App.GetService<ISysTimerService>().StartTimerJob();
        }

        #region 初始化数据库
        private void FreeSqlConfigure(IServiceCollection services)
        {
            #region 获取数据类型
            //Dictionary<Type, List<Type>> dbTypeDic = new Dictionary<Type, List<Type>>();
            //Dictionary<Type, List<Type>> dbSeedDic = new Dictionary<Type, List<Type>>();
            //foreach (var x in App.EffectiveTypes)
            //{
            //    if (!x.IsInterface && !x.IsAbstract)
            //    {
            //        if (x.FullName.StartsWith("Furion.DatabaseAccessor"))
            //        {
            //            continue;
            //        }
            //        var attr = x.GetCustomAttribute<DbLocatorAttribute>();
            //        Type locator = attr != null ? attr.GetDbLocator() : typeof(DefaultDbLocator);
            //        if (x.FullName.StartsWith("Furion.Extras.Admin.NET"))
            //        {
            //            locator = typeof(SysDbLocator);
            //        }
            //        if (typeof(IPrivateEntity).IsAssignableFrom(x))
            //        {
            //            if (!dbTypeDic.ContainsKey(locator))
            //            {
            //                dbTypeDic.Add(locator, new List<Type>());
            //            }
            //            dbTypeDic[locator].Add(x);

            //            continue;
            //        }
            //        foreach (var item in x.GetInterfaces())
            //        {
            //            if (item.IsGenericType && item.GetGenericTypeDefinition() == typeof(FreeSql.IEntitySeedData<>))
            //            {
            //                services.AddScoped(x);

            //                if (!dbSeedDic.ContainsKey(locator))
            //                {
            //                    dbSeedDic.Add(locator, new List<Type>());
            //                }
            //                dbSeedDic[locator].Add(x);
            //                break;
            //            }
            //        }
            //    }
            //}
            foreach (var name in DataBaseOptions.GetNodeNameList())
            {
                var configs = App.Configuration[$"PrivateEntity:{name}"];
                if (!string.IsNullOrEmpty(configs))
                {
                    var arr = configs.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                    var tys = App.EffectiveTypes.Where(x => arr.Contains(x.FullName)).ToArray();
                    if (tys.Length > 0)
                    {
                        foreach (var x in App.EffectiveTypes)
                        {
                            if (!x.IsInterface && !x.IsAbstract)
                            {
                                foreach (var iface in x.GetInterfaces())
                                {
                                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(FreeSql.IEntitySeedData<>)
                                    && tys.Contains(iface.GenericTypeArguments[0]))
                                    {
                                        services.AddScoped(typeof(FreeSql.IEntitySeedData<>).MakeGenericType(iface.GenericTypeArguments[0]), x);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var str= App.Configuration[$"PrivateEntity"];
           

            #endregion
            Action<FreeSqlBuilder> buildAction = builder =>
            {
                if (App.HostEnvironment.IsDevelopment())
                {
                    if (bool.TryParse(App.Configuration["UseAutoSyncStructure"],out bool used) && used)
                    {
                        builder.UseAutoSyncStructure(true);
                    }
                    builder.UseMonitorCommand(cmd =>
                           {
                               Console.WriteLine(cmd.CommandText + "\r\n\r\n" + SqlProfiler.ParameterFormat(cmd.CommandText, cmd.Parameters));
                               App.PrintToMiniProfiler("FreeSql", "Info", SqlProfiler.ParameterFormat(cmd.CommandText, cmd.Parameters));
                           });
                }
            };
            Action<IFreeSql> freesqlAction = db =>
            {
                if (App.HostEnvironment.IsDevelopment() && db.CodeFirst.IsAutoSyncStructure)
                {
                    var gType = db.GetType().GetInterface(typeof(IFreeSql<>).Name);
                    var locator = typeof(DefaultDbLocator);
                    if (gType != null)
                    {
                        var argTypes = gType.GetGenericArguments();
                        if (argTypes?.Length>0 && typeof(IDbLocator).IsAssignableFrom(argTypes[0]))
                        {
                            locator = argTypes[0];
                        }
                    }

                    var name = DataBaseOptions.GetConnectionName(locator);
                    if (!string.IsNullOrEmpty(name))
                    {
                        var configs = App.Configuration[$"PrivateEntity:{name}"];
                        if (!string.IsNullOrEmpty(configs))
                        {
                            var arr = configs.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                            var tys = App.EffectiveTypes.Where(x => arr.Contains(x.FullName)).ToArray();
                            if (tys.Length>0)
                            {
                                db.CodeFirst.SyncStructure(tys);
                                foreach (var item in tys)
                                {
                                    var ty = typeof(FreeSql.IEntitySeedData<>).MakeGenericType(item);
                                    var service = App.GetService(ty) as IEntitySeedData;
                                    service?.SetData(db);
                                }
                            }
                        }
                    }
                }
            };

            #region 配置freesql
            var options = App.GetConfig<DataBaseOptions>("DataBase");
            DataType dataType;
            DataType defaultDataType = DataType.Custom;
            if (options.DefaultConnection != null && options.DefaultConnection.Enable && Enum.TryParse(options.DefaultConnection.DataType, out dataType))
            {
                defaultDataType = dataType;
                services.AddFreeSql(options.DefaultConnection.ConnectionString, dataType, buildAction, freesqlAction);
                services.AddFreeSql<DefaultDbLocator>(options.DefaultConnection.ConnectionString, dataType, buildAction, freesqlAction);
            }
            if (options.TenantConnection != null && options.TenantConnection.Enable && Enum.TryParse(options.TenantConnection.DataType, out dataType))
            {
                services.AddFreeSql<TenantDbLocator>(options.TenantConnection.ConnectionString, dataType, buildAction, freesqlAction);
            }
            else
            {
                services.AddFreeSql<TenantDbLocator>(options.DefaultConnection.ConnectionString, defaultDataType, buildAction, freesqlAction);
            }
            if (options.LogDbConnection != null && options.LogDbConnection.Enable && Enum.TryParse(options.LogDbConnection.DataType, out dataType))
            {
                services.AddFreeSql<LogDbLocator>(options.LogDbConnection.ConnectionString, dataType, buildAction, freesqlAction);
            }
            else
            {
                services.AddFreeSql<LogDbLocator>(options.DefaultConnection.ConnectionString, defaultDataType, buildAction, freesqlAction);
            }
            if (options.SysDbConnection != null && options.SysDbConnection.Enable && Enum.TryParse(options.SysDbConnection.DataType, out dataType))
            {
                services.AddFreeSql<SysDbLocator>(options.SysDbConnection.ConnectionString, dataType, buildAction, freesqlAction);
            }
            else
            {
                services.AddFreeSql<SysDbLocator>(options.DefaultConnection.ConnectionString, defaultDataType, buildAction, freesqlAction);
            }
            services.AddFreeSqlRepository();

            // 添加全局 工作单元拦截器
            services.AddMvcFilter<FreeSqlUnitOfWorkFilter>();
            #endregion
        }
        #endregion
    }
}