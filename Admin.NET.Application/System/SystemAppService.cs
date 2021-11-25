using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using FreeSql;
using Furion.Extras.Admin.NET;
using Microsoft.AspNetCore.Authorization;
using Furion;

namespace Admin.NET.Application
{
    /// <summary>
    /// 系统服务接口
    /// </summary>
    //[ApiDescriptionSettings("自己的业务", Name = "Test", Order = 100)]
    public class SystemAppService : IDynamicApiController
    {
        private readonly ISystemService _systemService;

        public SystemAppService(ISystemService systemService)
        {
            _systemService = systemService;
        }

        /// <summary>
        /// 获取系统描述
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetDescription()
        {
            return _systemService.GetDescription();
        }
    }
}
