using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Services.Repository
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Runs the query returning a QueryResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static QueryResult<T> Run<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            return new QueryResult<T>
            {
                TotalCount = queryable.Count(),
                Result = queryable
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
            };
        }
    }
}