using EmployeeDirectory.Web.Models;
using EmployeeDirectory.Web.Services;
using EmployeeDirectory.Web.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using EmployeeDirectory.Web.Infrastructure.Repository;

namespace EmployeeDirectory.Web.Controllers
{
    [RoutePrefix("api/employees")]
    [Authorize]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _repo;
        private readonly IEmployeeService _empService;

        public EmployeeController()
        {
            _repo = new EmployeeDirectory.Web.Infrastructure.Repository.EmployeeRepository();
            _empService = new EmployeeService(_repo, HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
        }

        [Route("", Name = "GetEmployee")]
        public IHttpActionResult Get()
        {
            List<Employee> all = _repo.Get().ToList();
            return Ok(all);
        }

        [Route("{id:int:min(1)}")]
        public IHttpActionResult Get(int id)
        {
            Employee employee = _repo.GetById(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _empService.CreateEmployee(employee);
                return Created<EmployeeCreateResult>(Url.Link("GetEmployee", null) + "/" + result.Employee.EmployeeId, result); //TODO - fix uglyness in Location route
            }

            return BadRequest(ModelState);
        }

        //TODO - Authentication strategy - Rules: Admin or Employee as self - Custom attribute or code into the method?
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Put(int id, [FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.EmployeeId)
                {
                    return BadRequest();
                }

                try
                {
                    _repo.Update(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repo.GetById(employee.EmployeeId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

            return BadRequest(ModelState);
        }

        [Route("{id:int:min(1)}")]
        [Authorize(Roles = "Admin")] //TODO - Authentication strategy - can an employee delete themself?
        public IHttpActionResult Delete(int id)
        {
            Employee existingEmp = _repo.GetById(id);
            if (existingEmp != null)
            {
                _empService.DeleteEmployee(existingEmp);
                return StatusCode(HttpStatusCode.NoContent);
            }

            return NotFound();
        }


        //TODO - implement action method to set an Admin role
    }
}
