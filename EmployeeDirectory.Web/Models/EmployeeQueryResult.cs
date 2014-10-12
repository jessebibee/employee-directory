using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.Models
{
    public class QueryResult<T>
    {
        public QueryResult()
        {
            Result = new List<T>();
        }
        
        public int TotalCount { get; set; }

        public IEnumerable<T> Result { get; set; }
    }
}