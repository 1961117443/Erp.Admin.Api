
using FreeSql;
using Furion.DependencyInjection;
using Furion.Extras.Admin.NET;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.NET.Application
{
    public class SystemService : ISystemService, ITransient
    {
        private readonly IFreeSqlRepository<SysUser> repository;
        private readonly ISimpleRepository<LogDbLocator, SysLogOp> simpleRepository;

        public SystemService(IFreeSqlRepository<SysUser> repository,ISimpleRepository<LogDbLocator,SysLogOp> simpleRepository)
        {
            this.repository = repository;
            this.simpleRepository = simpleRepository;
        }
        public string GetDescription()
        {
            return "让 .NET 开发更简单，更通用，更流行。";
        }
    }
}