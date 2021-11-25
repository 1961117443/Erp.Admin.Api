using FreeSql;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 系统参数配置服务
    /// </summary>
    [ApiDescriptionSettings(Name = "Config", Order = 100)]
    public class SysConfigService : ISysConfigService, IDynamicApiController, ITransient
    {
        private readonly ISimpleRepository<SysDbLocator, SysConfig> _sysConfigRep;    // 参数配置表仓储
        private readonly ISysCacheService _sysCacheService;

        public SysConfigService(ISimpleRepository<SysDbLocator, SysConfig> sysConfigRep, ISysCacheService sysCacheService)
        {
            _sysConfigRep = sysConfigRep;
            _sysCacheService = sysCacheService;
        }

        /// <summary>
        /// 分页获取系统参数配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysConfig/page")]
        public async Task<dynamic> QueryConfigPageList([FromQuery] ConfigPageInput input)
        {
            var name = !string.IsNullOrEmpty(input.Name?.Trim());
            var code = !string.IsNullOrEmpty(input.Code?.Trim());
            var groupCode = !string.IsNullOrEmpty(input.GroupCode?.Trim());
            var configs = await _sysConfigRep
                                             .Where(name, u => u.Name.Contains(input.Name.Trim()))
                                             .Where(code, u => u.Code.Contains(input.Code.Trim()))
                                             .Where(groupCode, u => u.GroupCode.Contains(input.GroupCode.Trim()))
                                             .Where(u => u.Status != CommonStatus.DELETED).OrderBy(u => u.GroupCode)
                                             .ToPagedListAsync(input.PageNo, input.PageSize);
            return XnPageResult<SysConfig>.PageResult(configs);
        }

        /// <summary>
        /// 获取系统参数配置列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/sysConfig/list")]
        public async Task<dynamic> GetConfigList()
        {
            return await _sysConfigRep.Where(u => u.Status != CommonStatus.DELETED).ToListAsync();
        }

        /// <summary>
        /// 增加系统参数配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysConfig/add")]
        public async Task AddConfig(AddConfigInput input)
        {
            var isExist = await _sysConfigRep.AnyAsync(u => u.Name == input.Name || u.Code == input.Code);
            if (isExist)
                throw Oops.Oh(ErrorCode.D9000);

            var config = input.Adapt<SysConfig>();

            await _sysConfigRep.InsertAsync(config);
        }

        /// <summary>
        /// 删除系统参数配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysConfig/delete")]
        public async Task DeleteConfig(DeleteConfigInput input)
        {
            var config = await _sysConfigRep.FirstOrDefaultAsync(u => u.Id == input.Id);
            // 禁止删除系统参数
            if (config.SysFlag == YesOrNot.Y.ToString())
                throw Oops.Oh(ErrorCode.D9001);

            await _sysConfigRep.DeleteAsync(config);
        }

        /// <summary>
        /// 更新系统参数配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysConfig/edit")]
        public async Task UpdateConfig(UpdateConfigInput input)
        {
            var isExist = await _sysConfigRep.AnyAsync(u => (u.Name == input.Name || u.Code == input.Code) && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ErrorCode.D9000);

            var config = input.Adapt<SysConfig>();

            await _sysConfigRep.UpdateAsync(config, ignoreNullValues: true);
        }

        /// <summary>
        /// 获取系统参数配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysConfig/detail")]
        public async Task<SysConfig> GetConfig([FromQuery] QueryConfigInput input)
        {
            return await _sysConfigRep.FirstOrDefaultAsync(u => u.Id == input.Id);
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<dynamic> GetConfigCache(string code)
        {
            var value = await _sysCacheService.GetStringAsync(code);
            if (string.IsNullOrEmpty(value))
            {
                var config = await _sysConfigRep.FirstOrDefaultAsync(u => u.Code == code);
                value = config != null ? config.Value : "";
                await _sysCacheService.SetStringAsync(code, value);
            }
            return value;
        }

        /// <summary>
        /// 更新配置缓存
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task UpdateConfigCache(string code, object value)
        {
            await _sysCacheService.SetAsync(code, value);
        }

        /// <summary>
        /// 获取演示环境开关是否开启，默认为false
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> GetDemoEnvFlag()
        {
            var value = await GetConfigCache("DILON_DEMO_ENV_FLAG");
            return bool.Parse(value);
        }

        /// <summary>
        /// 获取验证码开关标识
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> GetCaptchaOpenFlag()
        {
            var value = await GetConfigCache("DILON_CAPTCHA_OPEN");
            return bool.Parse(value);
        }
    }
}