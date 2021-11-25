using System;
using System.Data;

namespace FreeSql
{
    /// <summary>
    /// FreeSql 工作单元配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class FreeSqlUnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FreeSqlUnitOfWorkAttribute()
        {
        }

        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.Unspecified;
    }
}
