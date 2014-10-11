using EmployeeDirectory.Web.Infrastructure;
using EmployeeDirectory.Web.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Web.App_Start
{
    public static class DbInitialization
    {
        /// <summary>
        /// Simple initialization of Identity three users and two employee records to match
        /// <param name="extraEmployeeCount">Additional users to create</param>
        /// </summary>
        public static void Initialize(int extraEmployeeCount)
        {
            Database.SetInitializer<IdentityContext>(new DbInitializer(extraEmployeeCount));
            IdentityContext identityCtx = new IdentityContext();
            identityCtx.Database.Initialize(false);
            identityCtx.Dispose();
        }
    }
}