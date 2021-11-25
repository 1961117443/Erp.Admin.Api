using FreeSql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 系统用户表种子数据
    /// </summary>
    public class SysUserSeedData : FreeSqlEntitySeedData<SysUser>, IEntitySeedData<SysUser>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<SysUser> HasData()
        {
            return new[]
            {
                new SysUser{TenantId=142307070918780, Id=142307070910551, Account="superAdmin", Name="超级管理员", NickName="superAdmin", Password="e10adc3949ba59abbe56e057f20f883e", AdminType=AdminType.SuperAdmin, Birthday=DateTime.Parse("1986-07-26 00:00:00"), Phone="18020030720", Sex=Gender.MALE, IsDeleted=false },
                new SysUser{TenantId=142307070918780, Id=142307070910552, Account="admin", Name="系统管理员", NickName="admin", Password="e10adc3949ba59abbe56e057f20f883e", AdminType=AdminType.Admin, Birthday=DateTime.Parse("1986-07-26 00:00:00"), Phone="18020030720", Sex=Gender.MALE, IsDeleted=false }
            };
        }
    }
}