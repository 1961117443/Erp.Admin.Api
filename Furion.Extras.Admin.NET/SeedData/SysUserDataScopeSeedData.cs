using FreeSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Furion.Extras.Admin.NET.SeedData
{
    public class SysUserDataScopeSeedData : FreeSqlEntitySeedData<SysUserDataScope>, IEntitySeedData<SysUserDataScope>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public override IEnumerable<SysUserDataScope> HasData()
        {
            return new[]
            {
                new SysUserDataScope{SysUserId=142307070910554, SysOrgId=142307070910547 }
            };
        }
    }
}