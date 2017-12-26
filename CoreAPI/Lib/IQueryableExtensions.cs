using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;


namespace CoreAPI.Lib
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
        {
            if (source == null)
                throw new ArgumentNullException("soruce");

            if (string.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            var sortExpression = string.Empty;
            //获取所有的排序字段，包括升序和降序
            var listSortBy = sortBy.Split(',');
            foreach(var item in listSortBy)
            {
                sortExpression += AdjustDirection(item) + ",";
            }
            sortExpression = sortExpression.Substring(0, sortExpression.Length - 1);

            try
            {
                source = source.OrderBy(sortExpression);
            }
            catch (ParseException ex)
            {
                //排序中可能包含了实体没有的字段
                throw ex;
            }

            return source;
        }

        private static string AdjustDirection(string item)
        {
            if (!item.Contains(' ')) //没有排序方向
                return item;

            var field = item.Split(' ')[0];
            var direction = item.Split(' ')[1];

            switch (direction)
            {
                case "asc":
                case "ascending":
                    return field + " ascending";

                case "desc":
                case "descending":
                    return field + " descending";

                default:
                    return field;
            }
        }
    }
}
