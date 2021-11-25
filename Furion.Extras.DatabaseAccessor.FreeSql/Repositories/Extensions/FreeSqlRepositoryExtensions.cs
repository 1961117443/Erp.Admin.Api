using FreeSql;
using FreeSql.Internal.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    ///
    /// </summary>
    public static partial class FreeSqlDbContextExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="entity"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public static T1 FirstOrDefault<T1>(this ISelect<T1> entity, Expression<Func<T1, bool>> whereExpression)
        {
            return entity.Where(whereExpression).First();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Task<T1> FirstOrDefaultAsync<T1>(this ISelect<T1> entity)
        {
            return entity.FirstAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="entity"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public static Task<T1> FirstOrDefaultAsync<T1>(this ISelect<T1> entity, Expression<Func<T1, bool>> whereExpression)
        {
            return entity.Where(whereExpression).FirstAsync();
        }

        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<T1> ToPagedList<T1>(this ISelect<T1> entity, int pageIndex, int pageSize)
                      where T1 : class, new()
        {
            return entity.ToPagedList(pageIndex, pageSize, u => u);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static FreeSqlPagedList<TDto> ToPagedList<T1, TDto>(this ISelect<T1> entity, int pageIndex, int pageSize)
                          where T1 : class, new()
                          where TDto : class, new()
        {
            return entity.ToPagedList<T1, TDto>(pageIndex, pageSize, u => new TDto());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, TReturn>(this ISelect<T1> entity, int pageIndex, int pageSize, Expression<Func<T1, TReturn>> select)
                          where T1 : class, new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList<TReturn>(select);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return new FreeSqlPagedList<TReturn>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = (int)totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }

        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<T1>> ToPagedListAsync<T1>(this ISelect<T1> entity, int pageIndex, int pageSize)
                      where T1 : class, new()
        {
            return await entity.ToPagedListAsync(pageIndex, pageSize, u => u);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<FreeSqlPagedList<TDto>> ToPagedListAsync<T1, TDto>(this ISelect<T1> entity, int pageIndex, int pageSize)
                          where T1 : class, new()
                          where TDto : class, new()
        {
            return await entity.ToPagedListAsync<T1, TDto>(pageIndex, pageSize, u => new TDto());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static async Task<FreeSqlPagedList<TDto>> ToPagedListAsync<T1, TDto>(this ISelect<T1> entity, int pageIndex, int pageSize, Expression<Func<T1, TDto>> select)
                          where T1 : class, new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return new FreeSqlPagedList<TDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = (int)totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="entity"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public static ISelect<T1> Where<T1>(this ISelect<T1> entity,bool condition, Expression<Func<T1, bool>> whereExpression)
        {
            return entity.WhereIf(condition,whereExpression);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="entity"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        //public static ISelect<T1> Search<T1, TSearchDto>(this ISelect<T1> entity, DynamicFilterInfo searchDto)
        //{
        //    return entity.WhereIf(condition, whereExpression);
        //}

        #region  扩展 IFreeSqlRepository<TEntity>
        public static IFreeSql AsDatabase<TEntity>(this IFreeSqlRepository<TEntity> repository) where TEntity : class, new()
        {
            return repository.DynamicDbContext as IFreeSql;
        }

        public static IUpdate<TEntity> AsUpdateable<TEntity>(this IFreeSqlRepository<TEntity> repository) where TEntity : class, new()
        {
            var service = repository as IBaseRepository<TEntity>;
            return service.UpdateDiy;
        } 
        #endregion
    }
}