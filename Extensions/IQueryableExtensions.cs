using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Core.Models;

namespace Vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj,
                                                        Dictionary<string, Expression<Func<T, object>>> collumnsMapping)
        {
            if(string.IsNullOrWhiteSpace(queryObj.SortBy) || !collumnsMapping.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(collumnsMapping[queryObj.SortBy]);
            else
                return query.OrderByDescending(collumnsMapping[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPagging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if(queryObj.PageSize <= 0)
                queryObj.PageSize = 10;
            if(queryObj.Page <= 0)
                queryObj.Page = 1;
                
            return query.Skip((queryObj.Page -1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}