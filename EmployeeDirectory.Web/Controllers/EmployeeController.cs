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
    //[Authorize]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _repo;
        private readonly IEmployeeService _empService;

        public EmployeeController()
        {
            _repo = new EmployeeDirectory.Web.Infrastructure.Repository.EmployeeRepository();
            _empService = new EmployeeService(_repo, HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
        }

        [Route("")]
        public IHttpActionResult Get(string search = null, Location? location = null, int page = 1, int pageSize = 25)
        {
            var result = new QueryResult<Employee>();

            if (search == null && !location.HasValue)
            {
                result.ResultCount = _repo.Get().Count();
                result.Result = _repo.Get()
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Skip((page - 1) * page)
                    .Take(pageSize)
                    .ToList();
            }
            else if (search == null && location.HasValue)
            {
                result.ResultCount = _repo.Get().Where(x => x.Location == location.Value).Count();
                result.Result = _repo.Get()
                    .Where(x => x.Location == location.Value)
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Skip((page - 1) * page)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                string searchQuery = HttpUtility.UrlDecode(search);
                if (searchQuery.Contains('@')) //Email
                {
                    result.ResultCount = _repo.Get().Where(x => x.Email.Contains(searchQuery)).Count();
                    result.Result = _repo.Get()
                        .Where(x => x.Email.Contains(searchQuery))
                        .OrderBy(x => x.LastName)
                        .ThenBy(x => x.FirstName)
                        .Skip((page - 1) * page)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    //first or last name
                    result.ResultCount = _repo.Get().Where(x => x.FirstName.Contains(searchQuery) || x.LastName.Contains(searchQuery)).Count();
                    result.Result = _repo.Get()
                        .Where(x => x.FirstName.Contains(searchQuery) || x.LastName.Contains(searchQuery))
                        .OrderBy(x => x.LastName)
                        .ThenBy(x => x.FirstName)
                        .Skip((page - 1) * page)
                        .Take(pageSize)
                        .ToList();
                }
            }

            return Ok(result);
        }

        [Route("{id:int:min(1)}", Name = "GetEmployee")]
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
