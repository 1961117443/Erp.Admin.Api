using Furion;
using FreeSql;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.ViewEngine;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furion.Extras.Admin.NET.Service.CodeGen
{
    /// <summary>
    /// 代码生成器服务
    /// </summary>
    [ApiDescriptionSettings(Name = "CodeGen", Order = 100)]
    public class CodeGenService : ICodeGenService, IDynamicApiController, ITransient
    {
        private readonly ISimpleRepository<SysDbLocator, SysCodeGen> _sysCodeGenRep; // 代码生成器仓储
        private readonly ICodeGenConfigService _codeGenConfigService;
        private readonly IViewEngine _viewEngine;

        private readonly ISimpleRepository<SysDbLocator, SysMenu> _sysMenuRep; // 菜单表仓储

        public CodeGenService(ISimpleRepository<SysDbLocator, SysCodeGen> sysCodeGenRep,
                              ICodeGenConfigService codeGenConfigService,
                              IViewEngine viewEngine,
                              ISimpleRepository<SysDbLocator, SysMenu> sysMenuRep)
        {
            _sysCodeGenRep = sysCodeGenRep;
            _codeGenConfigService = codeGenConfigService;
            _viewEngine = viewEngine;
            _sysMenuRep = sysMenuRep;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/codeGenerate/page")]
        public async Task<dynamic> QueryCodeGenPageList([FromQuery] CodeGenPageInput input)
        {
            var tableName = !string.IsNullOrEmpty(input.TableName?.Trim());
            var codeGens = await _sysCodeGenRep
                                               .Where(tableName, u => u.TableName.Contains(input.TableName.Trim()))
                                               .ToPagedListAsync(input.PageNo, input.PageSize);
            return XnPageResult<SysCodeGen>.PageResult(codeGens);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/codeGenerate/add")]
        public async Task AddCodeGen(AddCodeGenInput input)
        {
            var isExist = await _sysCodeGenRep.AnyAsync(u => u.TableName == input.TableName);
            if (isExist)
                throw Oops.Oh(ErrorCode.D1400);

            var codeGen = input.Adapt<SysCodeGen>();
            var newCodeGen= await _sysCodeGenRep.InsertAsync(codeGen);

            // 加入配置表中
            _codeGenConfigService.AddList(GetColumnList(input), newCodeGen);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("/codeGenerate/delete")]
        public async Task DeleteCodeGen(List<DeleteCodeGenInput> inputs)
        {
            if (inputs == null || inputs.Count < 1) return;

            var codeGenConfigTaskList = new List<Task>();
            inputs.ForEach(u =>
            {
                _sysCodeGenRep.Delete(u.Id);

                // 删除配置表中
                codeGenConfigTaskList.Add(_codeGenConfigService.Delete(u.Id));
            });
            await Task.WhenAll(codeGenConfigTaskList);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/codeGenerate/edit")]
        public async Task UpdateCodeGen(UpdateCodeGenInput input)
        {
            var isExist = await _sysCodeGenRep.AnyAsync(u => u.TableName == input.TableName && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ErrorCode.D1400);

            var codeGen = input.Adapt<SysCodeGen>();
            await _sysCodeGenRep.UpdateAsync(codeGen);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/codeGenerate/detail")]
        public async Task<SysCodeGen> GetCodeGen([FromQuery] QueryCodeGenInput input)
        {
            return await _sysCodeGenRep.FirstOrDefaultAsync(u => u.Id == input.Id);
        }

        /// <summary>
        /// 获取数据库表(实体)集合
        /// </summary>
        /// <returns></returns>
        [HttpGet("/codeGenerate/InformationList")]
        public List<TableOutput> GetTableList()
        {
            var entityInfos = _sysCodeGenRep.AsDatabase().DbFirst.GetTablesByDatabase();
            List<TableOutput> result = new List<TableOutput>();
            foreach (var item in entityInfos)
            {
                result.Add(new TableOutput()
                {
                    TableName = item.Name,
                    TableComment = item.Comment
                });
            }
            return result;
        }

        /// <summary>
        /// 根据表名获取列
        /// </summary>
        /// <returns></returns>
        [HttpGet("/codeGenerate/ColumnList/{tableName}")]
        public List<TableColumnOuput> GetColumnListByTableName(string tableName)
        {
            // 获取实体类型属性
            var entityType = _sysCodeGenRep.AsDatabase().DbFirst.GetTableByName(tableName);
            if (entityType == null) return null;

            var result = entityType.Columns.Select(u => new TableColumnOuput
            {
                ColumnName = u.Name,
                ColumnKey = u.IsPrimary.ToString(),
                DataType = u.DbTypeText,
                NetType = u.CsType.Name,
                ColumnComment = u.Coment
            }).ToList();
            return result;
        }

        /// <summary>
        /// 获取数据表列（实体属性）集合
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public List<TableColumnOuput> GetColumnList([FromQuery] AddCodeGenInput input)
        {
            // 获取实体类型属性
            IBaseRepository<SysCodeGen> baseRepository = _sysCodeGenRep as IBaseRepository<SysCodeGen>;
            var entityType = baseRepository.Orm.DbFirst.GetTableByName(input.TableName);
            if (entityType == null) return null;


            var result = entityType.Columns.Select(u => new TableColumnOuput
            {
                ColumnName = u.Name,
                ColumnKey = u.IsPrimary.ToString(),
                DataType = u.DbTypeText,
                NetType = u.CsType.Name,
                ColumnComment = u.Coment
            }).ToList();
            return result;
        }

        /// <summary>
        /// 代码生成_本地项目
        /// </summary>
        /// <returns></returns>
        [HttpPost("/codeGenerate/runLocal")]
        public async Task RunLocal(SysCodeGen input)
        {
            var templatePathList = GetTemplatePathList();
            var targetPathList = GetTargetPathList(input);
            for (var i = 0; i < templatePathList.Count; i++)
            {
                var tContent = File.ReadAllText(templatePathList[i]);

                var tableFieldList = await _codeGenConfigService.List(new CodeGenConfig() { CodeGenId = input.Id }); // 字段集合
                if (i >= 5) // 适应前端首字母小写
                {
                    tableFieldList.ForEach(u =>
                    {
                        u.ColumnName = u.ColumnName.Substring(0, 1).ToLower() + u.ColumnName[1..];
                    });
                }

                var queryWhetherList = tableFieldList.Where(u => u.QueryWhether == YesOrNot.Y.ToString()).ToList(); // 前端查询集合
                var tResult = _viewEngine.RunCompileFromCached(tContent, new
                {
                    input.AuthorName,
                    input.BusName,
                    input.NameSpace,
                    input.ProName,
                    ClassName = input.TableName,
                    QueryWhetherList = queryWhetherList,
                    TableField = tableFieldList                    
                });

                var dirPath = new DirectoryInfo(targetPathList[i]).Parent.FullName;
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                File.WriteAllText(targetPathList[i], tResult, Encoding.UTF8);
            }

            await AddMenu(input.TableName, input.BusName, input.MenuApplication, input.MenuPid);
        }

        private async Task AddMenu(string className, string busName, string application, long pid)
        {
            // 定义菜单编码前缀
            var codePrefix = "dilon_" + className.ToLower();

            // 先删除该表已生成的菜单列表
            var menus = await _sysMenuRep.Where(u => u.Code == codePrefix || u.Code.StartsWith(codePrefix + "_")).ToListAsync();
            await _sysMenuRep.DeleteAsync(menus);

            // 如果 pid 为 0 说明为顶级菜单, 需要创建顶级目录
            if (pid == 0)
            {
                // 目录
                var menuType0 = new SysMenu
                {
                    Pid = 0,
                    Pids = "[0],",
                    Name = busName + "管理",
                    Code = codePrefix,
                    Type = MenuType.DIR,
                    Icon = "robot",
                    Router = "/" + className.ToLower(),
                    Component = "PageView",
                    Application = application
                };
                pid = (await _sysMenuRep.InsertAsync(menuType0)).Id;
            }
            // 由于后续菜单会有修改, 需要判断下 pid 是否存在, 不存在报错
            else if (!await _sysMenuRep.AnyAsync(e => e.Id == pid))
                throw Oops.Oh(ErrorCode.D1505);

            // 菜单
            var menuType1 = new SysMenu
            {
                Pid = pid,
                Pids = "[0],[" + pid + "],",
                Name = busName + "管理",
                Code = codePrefix + "_mgr",
                Type = MenuType.MENU,
                Router = "/" + className.ToLower(),
                Component = "main/" + className + "/index",
                Application = application,
                OpenType = MenuOpenType.COMPONENT
            };
            var pid1 = (await _sysMenuRep.InsertAsync(menuType1)).Id;

            // 按钮-page
            var menuType2 = new SysMenu
            {
                Pid = pid1,
                Pids = "[0],[" + pid + "],[" + pid1 + "],",
                Name = busName + "查询",
                Code = codePrefix + "_mgr_page",
                Type = MenuType.BTN,
                Permission = className + ":page",
                Application = application,
            };
            await _sysMenuRep.InsertAsync(menuType2);

            // 按钮-detail
            var menuType2_1 = new SysMenu
            {
                Pid = pid1,
                Pids = "[0],[" + pid + "],[" + pid1 + "],",
                Name = busName + "详情",
                Code = codePrefix + "_mgr_detail",
                Type = MenuType.BTN,
                Permission = className + ":detail",
                Application = application,
            };
            await _sysMenuRep.InsertAsync(menuType2_1);

            // 按钮-add
            var menuType2_2 = new SysMenu
            {
                Pid = pid1,
                Pids = "[0],[" + pid + "],[" + pid1 + "],",
                Name = busName + "增加",
                Code = codePrefix + "_mgr_add",
                Type = MenuType.BTN,
                Permission = className + ":add",
                Application = application,
            };
            await _sysMenuRep.InsertAsync(menuType2_2);

            // 按钮-delete
            var menuType2_3 = new SysMenu
            {
                Pid = pid1,
                Pids = "[0],[" + pid + "],[" + pid1 + "],",
                Name = busName + "删除",
                Code = codePrefix + "_mgr_delete",
                Type = MenuType.BTN,
                Permission = className + ":delete",
                Application = application,
            };
            await _sysMenuRep.InsertAsync(menuType2_3);

            // 按钮-edit
            var menuType2_4 = new SysMenu
            {
                Pid = pid1,
                Pids = "[0],[" + pid + "],[" + pid1 + "],",
                Name = busName + "编辑",
                Code = codePrefix + "_mgr_edit",
                Type = MenuType.BTN,
                Permission = className + ":edit",
                Application = application,
            };
            await _sysMenuRep.InsertAsync(menuType2_4);
        }

        /// <summary>
        /// 获取模板文件路径集合
        /// </summary>
        /// <returns></returns>
        private List<string> GetTemplatePathList()
        {
            var templatePath = App.WebHostEnvironment.WebRootPath + @"\Template\";
            return new List<string>()
            {
                templatePath + "Service.cs.vm",
                templatePath + "IService.cs.vm",
                templatePath + "Input.cs.vm",
                templatePath + "Output.cs.vm",
                templatePath + "Dto.cs.vm",
                templatePath + "index.vue.vm",
                templatePath + "addForm.vue.vm",
                templatePath + "editForm.vue.vm",
                templatePath + "manage.js.vm",
            };
        }

        /// <summary>
        /// 设置生成文件路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<string> GetTargetPathList(SysCodeGen input)
        {
            var backendPath = new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.FullName + @"\" + input.NameSpace + @"\Service\" + input.TableName + @"\";
            var servicePath = backendPath + input.TableName + "Service.cs";
            var iservicePath = backendPath + "I" + input.TableName + "Service.cs";
            var inputPath = backendPath + @"Dto\" + input.TableName + "Input.cs";
            var outputPath = backendPath + @"Dto\" + input.TableName + "Output.cs";
            var viewPath = backendPath + @"Dto\" + input.TableName + "Dto.cs";
            var frontendPath = new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.Parent.FullName + @"\frontend\src\views\main\";
            var indexPath = frontendPath + input.TableName + @"\index.vue";
            var addFormPath = frontendPath + input.TableName + @"\addForm.vue";
            var editFormPath = frontendPath + input.TableName + @"\editForm.vue";
            var apiJsPath = new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.Parent.FullName + @"\frontend\src\api\modular\main\" + input.TableName + "Manage.js";

            return new List<string>()
            {
                servicePath,
                iservicePath,
                inputPath,
                outputPath,
                viewPath,
                indexPath,
                addFormPath,
                editFormPath,
                apiJsPath
            };
        }
    }
}