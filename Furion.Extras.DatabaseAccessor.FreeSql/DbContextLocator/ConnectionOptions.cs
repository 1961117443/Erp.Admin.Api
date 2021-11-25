using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    public class ConnectionOptions<TDb> : ConnectionOptions where TDb:IDbLocator
    {

    }
    public class ConnectionOptions
    {
        /// <summary>
        /// 数据库类型 DataType
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 具体的连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public string[] SlaveConnectionStrings { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable
        {
            get
            {
                return !string.IsNullOrEmpty(DataType) && !string.IsNullOrEmpty(ConnectionString);
            }
        }
    }
}
