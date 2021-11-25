using FreeSql;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 访问日志服务
    /// </summary>
    [ApiDescriptionSettings(Name = "VisLog", Order = 100)]
    public class SysVisLogService : ISysVisLogService, IDynamicApiController, ITransient
    {
        private readonly ISimpleRepository<LogDbLocator, SysLogVis> _sysVisLogRep;  // 访问日志表仓储

        public SysVisLogService(ISimpleRepository<LogDbLocator, SysLogVis> sysVisLogRep)
        {
            _sysVisLogRep = sysVisLogRep;
        }

        /// <summary>
        /// 分页查询访问日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysVisLog/page")]
        public async Task<dynamic> QueryVisLogPageList([FromQuery] VisLogPageInput input)
        {
            var name = !string.IsNullOrEmpty(input.Name?.Trim());
            var success = !string.IsNullOrEmpty(input.Success.ToString());
            var searchBeginTime = !string.IsNullOrEmpty(input.SearchBeginTime?.Trim());
            var visLogs = await _sysVisLogRep
                                             .Where(name, u => u.Name.Contains(input.Name.Trim()))
                                             .Where(input.VisType >= 0, u => u.VisType == input.VisType)
                                             .Where(success, u => u.Success == input.Success)
                                             .Where(searchBeginTime, u => u.VisTime >= DateTime.Parse(input.SearchBeginTime.Trim()) &&
                                                                     u.VisTime <= DateTime.Parse(input.SearchEndTime.Trim()))
                                             .OrderByDescending(u => u.Id)
                                             .ToPagedListAsync<SysLogVis, VisLogOutput>(input.PageNo, input.PageSize);
            return XnPageResult<VisLogOutput>.PageResult(visLogs);
        }

        /// <summary>
        /// 清空访问日志
        /// </summary>
        /// <returns></returns>
        [HttpPost("/sysVisLog/delete")]
        public async Task ClearVisLog()
        {
            var visLogs = await _sysVisLogRep.Entities.ToListAsync();
            await _sysVisLogRep.DeleteAsync(visLogs);
        }
    }
}