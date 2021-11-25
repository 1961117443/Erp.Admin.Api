using FreeSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Furion.Extras.Admin.NET.SeedData
{
    public class SysUserRoleSeedData : FreeSqlEntitySeedData<SysUserRole>, IEntitySeedData<SysUserRole>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public override IEnumerable<SysUserRole> HasData()
        {
            return new[]
            {
                // 租户管理员默认管理员角色
                new SysUserRole {SysUserId = 142307070910552, SysRoleId = 142307070910556},
                new SysUserRole {SysUserId = 142307070910554, SysRoleId = 142307070910556}
            };
        }
    }
}