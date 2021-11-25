using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// FreeSql 工作单元拦截器
    /// </summary>
    public class FreeSqlUnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 过滤器排序
        /// </summary>
        internal const int FilterOrder = 9999;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// FreeSql工作单元管理器
        /// </summary>
        private readonly UnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        public FreeSqlUnitOfWorkFilter(UnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"> </param>
        /// <param name="next"> </param>
        /// <returns> </returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取动作方法描述器
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            // 判断是否贴有工作单元特性
            if (!method.IsDefined(typeof(FreeSqlUnitOfWorkAttribute), true))
            {
                // 调用方法
                _ = await next();
            }
            else
            {
                var attribute = (method.GetCustomAttributes(typeof(FreeSqlUnitOfWorkAttribute), true).FirstOrDefault() as FreeSqlUnitOfWorkAttribute);

                _unitOfWork = _unitOfWorkManager.Begin(attribute.Propagation, attribute.IsolationLevel);

                // 调用方法
                var resultContext = await next();

                // 判断是否异常

                if (resultContext.Exception == null)
                {
                    try
                    {
                        _unitOfWork.Commit();
                    }
                    catch
                    {
                        _unitOfWork.Rollback();
                    }
                    finally
                    {
                        _unitOfWork.Dispose();
                    }
                }
                else
                {
                    // 回滚事务
                    _unitOfWork.Rollback();
                    _unitOfWork.Dispose();
                }
            }
        }
    }
}
