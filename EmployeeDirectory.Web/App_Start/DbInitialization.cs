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
        /// </summary>
        public static void InitializeIdentityUsers()
        {
            Database.SetInitializer<IdentityContext>(new IdentityDbInitializer());
            IdentityContext identityCtx = new IdentityContext();
            identityCtx.Database.Initialize(false);
            identityCtx.Dispose();
        }

        /// <summary>
        /// Initialize additional employee records (2 are created with Identity users).  A hydrater is used to generate records
        /// </summary>
        public static void InitializeEmployeeRecords(int employeeCount)
        {

        }
    }
}