using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Services.Repository
{
    public static class EmployeeQueryExtensions
    {
        /// <summary>
        /// Parses the search query and adds predicates on email (if an '@' character exists) or first/last name
        /// </summary>
        /// <param name="searchQuery">free-form query</param>
        public static IQueryable<Employee> Search(this IQueryable<Employee> queryable, string searchQuery)
        {
            if (searchQuery != null && searchQuery.Trim().Length > 0)
            {
                if (searchQuery.Contains('@'))
                {
                    //by email only
                    return queryable.Where(x => x.Email.Contains(searchQuery));
                }
                else
                {
                    //by first/last name
                    return queryable.Where(x => x.FirstName.Contains(searchQuery) || x.LastName.Contains(searchQuery));
                }
            }
            
            return queryable;
        }

        /// <summary>
        /// Adds a Location predicate to the Employee query if the location has a value
        /// </summary>
        public static IQueryable<Employee> Location(this IQueryable<Employee> queryable, Location? location)
        {
            if (location.HasValue)
            {
                return queryable.Where(x => x.Location == location);
            }

            return queryable;
        }

        /// <summary>
        /// Runs the query returning a QueryResult
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static QueryResult<Employee> Run(this IQueryable<Employee> queryable, int page, int pageSize)
        {
            return queryable.Run<Employee>(page, pageSize);
        }
    }
}