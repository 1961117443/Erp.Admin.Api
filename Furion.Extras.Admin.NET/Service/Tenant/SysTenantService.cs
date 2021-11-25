using FreeSql;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ApiDescriptionSettings(Name = "Tenant", Order = 100)]
    public class SysTenantService : ISysTenantService, IDynamicApiController, ITransient
    {
        private readonly ISimpleRepository<TenantDbLocator, SysTenant> _sysTenantRep;    // 租户表仓储
        private readonly ISimpleRepository<SysDbLocator, SysUser> _sysUserRep;

        private readonly ISysRoleMenuService _sysRoleMenuService;
        private readonly ISysUserRoleService _sysUserRoleService;
        private readonly ISimpleRepository<SysDbLocator, SysOrg> _orgRep;
        private readonly ISimpleRepository<SysDbLocator, SysRole> _sysRoleRep;
        private readonly ISimpleRepository<SysDbLocator, SysUser> _userRep;
        private readonly ISimpleRepository<SysDbLocator, SysEmp> _empRep;

        public SysTenantService(ISimpleRepository<TenantDbLocator,SysTenant> sysTenantRep,
                                ISimpleRepository<SysDbLocator, SysUser> sysUserService,
                                ISysRoleMenuService sysRoleMenuService,
                                ISysUserRoleService sysUserRoleService,
                                ISimpleRepository<SysDbLocator,SysOrg> orgRep,
                                ISimpleRepository<SysDbLocator,SysRole> roleRep,
                                ISimpleRepository<SysDbLocator,SysUser> userRep,
                                ISimpleRepository<SysDbLocator,SysEmp> empRep)
        {
            _sysTenantRep = sysTenantRep;
            _sysUserRep = sysUserService;
            _sysRoleMenuService = sysRoleMenuService;
            _sysUserRoleService = sysUserRoleService;
            _orgRep = orgRep;
            _sysRoleRep = roleRep;
            _userRep = userRep;
            _empRep = empRep;
        }

        /// <summary>
        /// 分页查询租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysTenant/page")]
        public async Task<dynamic> QueryTenantPageList([FromQuery] TenantPageInput input)
        {
            var name = !string.IsNullOrEmpty(input.Name?.Trim());
            var host = !string.IsNullOrEmpty(input.Host?.Trim());
            var tenants = await _sysTenantRep.Where(name, u => u.Name.Contains(input.Name.Trim()))
                                             .ToPagedListAsync<SysTenant, TenantOutput>(input.PageNo, input.PageSize);
            return XnPageResult<TenantOutput>.PageResult(tenants);
        }

        /// <summary>
        /// 增加租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTenant/add")]
        public async Task AddTenant(AddTenantInput input)
        {
            var isExist = await _sysTenantRep.Entities.AnyAsync(u => u.Name == input.Name || u.Email == input.Email);
            if (isExist)
                throw Oops.Oh(ErrorCode.D1300);

            var tenant = input.Adapt<SysTenant>();
            var newTenant =await _sysTenantRep.InsertAsync(tenant);
            await InitNewTenant(newTenant);
        }

        /// <summary>
        /// 新增租户时，初始化数据
        /// </summary>
        /// <param name="newTenant"></param>
        public async Task InitNewTenant(SysTenant newTenant)
        {
            long tenantId = newTenant.Id;
            string email = newTenant.Email;
            string companyName = newTenant.Name;
            // 初始化公司（组织结构）
            var newOrg = new SysOrg
            {
                TenantId = tenantId,
                Pid = 0,
                Pids = "[0],",
                Name = companyName,
                Code = companyName,
                Contacts = newTenant.AdminName,
                Tel = newTenant.Phone
            };
            newOrg = await _orgRep.InsertAsync(newOrg);

            // 初始化角色
            var newRole = new SysRole
            {
                TenantId = tenantId,
                Code = "sys_manager_role",
                Name = "系统管理员",
                DataScopeType = DataScopeType.ALL
            };
            newRole = await _sysRoleRep.InsertAsync(newRole);

            // 初始化租户管理员
            var newUser = new SysUser
            {
                TenantId = tenantId,
                Account = email,
                Password = MD5Encryption.Encrypt(CommonConst.DEFAULT_PASSWORD),
                Name = newTenant.AdminName,
                NickName = newTenant.AdminName,
                Email = newTenant.Email,
                Phone = newTenant.Phone,
                AdminType = AdminType.Admin
            };
            newUser = await _userRep.InsertAsync(newUser);

            // 初始化职工
            var emp= new SysEmp
            {
                Id = newUser.Id,
                JobNum = "D001",
                OrgId = newOrg.Id,
                OrgName = newOrg.Name
            };
            await _empRep.InsertAsync(emp);
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTenant/delete")]
        [FreeSqlUnitOfWork]
        public async Task DeleteTenant(DeleteTenantInput input)
        {
            var tenant = await _sysTenantRep.FirstOrDefaultAsync(u => u.Id == input.Id);
            await _sysTenantRep.DeleteAsync(tenant);

            var users = await _userRep.Where(u => u.TenantId == input.Id).ToListAsync();
            // 超级管理员所在租户认为是默认租户
            if (users.Any(u => u.AdminType == AdminType.SuperAdmin))
                throw Oops.Oh(ErrorCode.D1023);

            // 删除与租户相关的表数据
            await _userRep.DeleteAsync(users);

            var userIds = users.Select(u => u.Id).ToList();

            var userRoles = await _sysTenantRep.Change<SysUserRole>().DeleteAsync(u => userIds.Contains(u.SysUserId));

            var userDataScopes = await _sysTenantRep.Change<SysUserDataScope>().DeleteAsync(u => userIds.Contains(u.SysUserId));

            var emps = await _empRep.DeleteAsync(u => userIds.Contains(u.Id));

            var emppos = await _sysTenantRep.Change<SysEmpPos>().DeleteAsync(u => userIds.Contains(u.SysEmpId));

            var empexts = await _sysTenantRep.Change<SysEmpExtOrgPos>().DeleteAsync(u => userIds.Contains(u.SysEmpId));

            var roles = await _sysRoleRep.Where(u => u.TenantId == input.Id).ToListAsync();
            await _sysRoleRep.DeleteAsync(roles);

            var roleIds = roles.Select(u => u.Id).ToList();
            var roleMenus = await _sysTenantRep.Change<SysRoleMenu>().DeleteAsync(u => roleIds.Contains(u.SysRoleId));

            var roleDataScopes = await _sysTenantRep.Change<SysRoleDataScope>().DeleteAsync(u => roleIds.Contains(u.SysRoleId));

            var orgs = await _orgRep.DeleteAsync(u => u.TenantId == input.Id);

            var pos = await _sysTenantRep.Change<SysPos>().DeleteAsync(u => u.TenantId == input.Id);
        }

        /// <summary>
        /// 更新租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTenant/edit")]
        public async Task UpdateTenant(UpdateTenantInput input)
        {
            var isExist = await _sysTenantRep.Entities.AnyAsync(u => (u.Name == input.Name || u.Email == input.Email) && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ErrorCode.D1300);

            var tenant = input.Adapt<SysTenant>();
            await _sysTenantRep.UpdateAsync(tenant, true);
        }

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysTenant/detail")]
        public async Task<SysTenant> GetTenant([FromQuery] QueryTenantInput input)
        {
            return await _sysTenantRep.Entities.FirstOrDefaultAsync(u => u.Id == input.Id);
        }

        /// <summary>
        /// 授权租户管理员角色菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTenant/grantMenu")]
        public async Task GrantMenu(GrantRoleMenuInput input)
        {
            var tenantAdminUser = await GetTenantAdminUser(input.Id);
            if (tenantAdminUser == null) return;
            // 这里传false，就不会走全局tenantId过滤。true的话查不到数据，当前功能为超级管理员使用
            var roleIds = await _sysUserRoleService.GetUserRoleIdList(tenantAdminUser.Id,false);
            input.Id = roleIds[0]; // 重置租户管理员角色Id
            await _sysRoleMenuService.GrantMenu(input);
        }

        /// <summary>
        /// 获取租户管理员角色拥有菜单Id集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/sysTenant/ownMenu")]
        public async Task<List<long>> OwnMenu([FromQuery] QueryTenantInput input)
        {
            var tenantAdminUser = await GetTenantAdminUser(input.Id);
            if (tenantAdminUser == null) return new List<long>();
            var roleIds = await _sysUserRoleService.GetUserRoleIdList(tenantAdminUser.Id);
            var tenantAdminRoleId = roleIds[0]; // 租户管理员角色Id
            return await _sysRoleMenuService.GetRoleMenuIdList(new List<long> { tenantAdminRoleId });
        }

        /// <summary>
        /// 重置租户管理员密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTenant/resetPwd")]
        public async Task ResetUserPwd(QueryTenantInput input)
        {
            var tenantAdminUser = await GetTenantAdminUser(input.Id);
            tenantAdminUser.Password = MD5Encryption.Encrypt(CommonConst.DEFAULT_PASSWORD);
            await _sysUserRep.UpdateIncludeAsync(tenantAdminUser, a => new { a.Password });
        }

        /// <summary>
        /// 获取租户管理员用户
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task<SysUser> GetTenantAdminUser(long tenantId)
        {
            return await _sysUserRep.Where(u => u.TenantId == tenantId, false)
                                    .Where(u => u.AdminType == AdminType.Admin).FirstOrDefaultAsync();
        }
    }
}