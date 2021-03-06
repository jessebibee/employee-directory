﻿using EmployeeDirectory.Web.Models;
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
using EmployeeDirectory.Web.Services.Repository;

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

        [Route("")]
        public IHttpActionResult Get(string search = null, Location? location = null, int page = 1, int pageSize = 25)
        {
            QueryResult<Employee> result = _repo.AsQueryable()
                .Location(location)
                .Search(search)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Run(page, pageSize);

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
                EmployeeCreateResult result = await _empService.CreateEmployee(employee);

                if (result.Succeeded)
                {
                    return Created<EmployeeCreateResult>(Url.Link("GetEmployee", new { id = result.Employee.EmployeeId }), result);
                }
                else
                {
                    if (result.UserAlreadyExists)
                    {
                        return Conflict(); //409
                    }

                    return InternalServerError();
                }
            }

            return BadRequest(ModelState);
        }

        [Route("{id:int:min(1)}")]
        [AuthorizeAdminOrSelf]
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
        [AuthorizeAdminOrSelf]
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
    }
}
