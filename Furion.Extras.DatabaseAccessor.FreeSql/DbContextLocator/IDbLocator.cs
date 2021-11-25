using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 租户数据库
    /// </summary>
    public sealed class TenantDbLocator : IDbLocator
    {
    }
    /// <summary>
    /// 业务数据库
    /// </summary>
    public sealed class DefaultDbLocator : IDbLocator
    {
    }
    /// <summary>
    /// 内置数据库
    /// </summary>
    public sealed class SysDbLocator : IDbLocator
    {
    }
    /// <summary>
    /// 日志数据库
    /// </summary>
    public sealed class LogDbLocator : IDbLocator
    {
    }

    public interface IDbLocator
    {
    }
}
