using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 分页拓展类
    /// </summary>
    public static class PagedQueryableExtensions
    {
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, TReturn>(this ISelect<T1, T2> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, TReturn>> select)
            where T2 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, TReturn>(this ISelect<T1, T2, T3> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, TReturn>> select)
            where T2 : class
            where T3 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, TReturn>(this ISelect<T1, T2, T3, T4> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, TReturn>(this ISelect<T1, T2, T3, T4, T5> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="T15"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where T15 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="T15"> </typeparam>
        /// <typeparam name="T16"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static FreeSqlPagedList<TReturn> ToPagedList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where T15 : class
            where T16 : class
            where TReturn : new()
        {
            var items = entity.Count(out long totalCount).Page(pageIndex, pageSize).ToList(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> 选择的列 </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, TReturn>(this ISelect<T1, T2> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, TReturn>> select)
            where T2 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, TReturn>(this ISelect<T1, T2, T3> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, TReturn>> select)
            where T2 : class
            where T3 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, TReturn>(this ISelect<T1, T2, T3, T4> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, TReturn>(this ISelect<T1, T2, T3, T4, T5> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="T15"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where T15 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <typeparam name="T3"> </typeparam>
        /// <typeparam name="T4"> </typeparam>
        /// <typeparam name="T5"> </typeparam>
        /// <typeparam name="T6"> </typeparam>
        /// <typeparam name="T7"> </typeparam>
        /// <typeparam name="T8"> </typeparam>
        /// <typeparam name="T9"> </typeparam>
        /// <typeparam name="T10"> </typeparam>
        /// <typeparam name="T11"> </typeparam>
        /// <typeparam name="T12"> </typeparam>
        /// <typeparam name="T13"> </typeparam>
        /// <typeparam name="T14"> </typeparam>
        /// <typeparam name="T15"> </typeparam>
        /// <typeparam name="T16"> </typeparam>
        /// <typeparam name="TReturn"> </typeparam>
        /// <param name="entity"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="select"> </param>
        /// <returns> </returns>
        public static async Task<FreeSqlPagedList<TReturn>> ToPagedListAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(this ISelect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> entity, int pageIndex, int pageSize, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>> select)
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
            where T12 : class
            where T13 : class
            where T14 : class
            where T15 : class
            where T16 : class
            where TReturn : new()
        {
            var items = await entity.Count(out long totalCount).Page(pageIndex, pageSize).ToListAsync(select);
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
    }
}