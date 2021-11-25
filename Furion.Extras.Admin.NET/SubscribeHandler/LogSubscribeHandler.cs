using Furion;
using FreeSql;
using Furion.DependencyInjection;
using Furion.EventBus;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 日志订阅处理
    /// </summary>
    public class LogSubscribeHandler : ISubscribeHandler
    {
        public LogSubscribeHandler()
        {
        }

        [SubscribeMessage("create:oplog")]
        public void CreateOpLog(string eventId, object payload)
        {
            SysLogOp log = (SysLogOp)payload;
            Scoped.Create((_, scope) =>
            {
                var _rep = App.GetService<ISimpleRepository<LogDbLocator, SysLogOp>>(scope.ServiceProvider);
                _rep.Insert(log);
            });
        }

        [SubscribeMessage("create:exlog")]
        public void CreateExLog(string eventId, object payload)
        {
            SysLogEx log = (SysLogEx)payload;
            Scoped.Create((_, scope) =>
            {
                var _rep = App.GetService<ISimpleRepository<LogDbLocator, SysLogEx>>(scope.ServiceProvider);
                _rep.Insert(log);
            });
        }

        [SubscribeMessage("create:vislog")]
        public void CreateVisLog(string eventId, object payload)
        {
            SysLogVis log = (SysLogVis)payload;
            Scoped.Create((_, scope) =>
            {
                var _rep = App.GetService<ISimpleRepository<LogDbLocator, SysLogVis>>(scope.ServiceProvider);
                _rep.Insert(log);
            });
        }
    }
}