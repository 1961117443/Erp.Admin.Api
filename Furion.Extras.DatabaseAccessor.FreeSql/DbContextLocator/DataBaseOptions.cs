using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public class DataBaseOptions
    {
        /// <summary>
        /// 业务数据库
        /// </summary>
        public ConnectionOptions<DefaultDbLocator> DefaultConnection { get; set; }
        /// <summary>
        /// 租户数据库
        /// </summary>
        public ConnectionOptions<TenantDbLocator> TenantConnection { get; set; }
        /// <summary>
        /// 配置数据库
        /// </summary>
        public ConnectionOptions<SysDbLocator> SysDbConnection { get; set; }
        /// <summary>
        /// 日志数据库
        /// </summary>
        public ConnectionOptions<LogDbLocator> LogDbConnection { get; set; }

        public static string GetConnectionName(Type type)
        {
            string name = "";
            if (typeof(IDbLocator).IsAssignableFrom(type) && locators.ContainsKey(type))
            {
                name = locators[type];
            }
            return name;
        }

        public static string[] GetNodeNameList()
        {
            return locators.Select(x => x.Value).ToArray();
        }

        private static Dictionary<Type, string> locators = new Dictionary<Type, string>();

        static DataBaseOptions()
        {
            var props = typeof(DataBaseOptions).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsGenericType)
                {
                    var arg = prop.PropertyType.GenericTypeArguments[0];

                    if (typeof(IDbLocator).IsAssignableFrom(arg) && !locators.ContainsKey(arg))
                    {
                        locators.Add(arg, prop.Name);
                    }
                }
            }
        }
    }
}
