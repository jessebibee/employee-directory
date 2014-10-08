using EmployeeDirectory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeDirectory.Web.Controllers
{
    [RoutePrefix("api/employees")]
    //[Authorize]
    public class EmployeeController : ApiController
    {
        //static in-memory data store for now...
        private static List<Employee> _employees = new List<Employee> {
                new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Email = "johnd@corp.com", JobTitle = "Software Engineer", Location = "Austin", Phone = "4254567890" },
                new Employee { EmployeeId = 2, FirstName = "John", LastName = "Smith", Email = "johns@corp.com", JobTitle = "DevOps Engineer", Location = "Seattle", Phone = "5124123154" },
                new Employee { EmployeeId = 3, FirstName = "John", LastName = "Adams", Email = "johna@corp.com", JobTitle = "Business Analyst", Location = "Redmond", Phone = "2064567890" }
            };

        [Route("", Name = "GetEmployee")]
        public IHttpActionResult Get()
        {
            return Ok(_employees);
        }

        [Route("{id:int:min(1)}")]
        public IHttpActionResult Get(int id)
        {
            if (_employees.Any(x => x.EmployeeId == id))
            {
                return Ok(_employees.First(x => x.EmployeeId == id));
            }
            return NotFound();
        }

        //TODO - Authentication strategy - Rules Admin or Employee as well - Custom attribute?
        [Route("")]
        public IHttpActionResult Post([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                int employeeId = 0;
                if (_employees.Any())
                {
                    employeeId = _employees.Max(x => x.EmployeeId);
                }
                employee.EmployeeId = employeeId + 1;

                _employees.Add(employee);
                return Created<Employee>(Url.Link("GetEmployee", null) + "/" + employee.EmployeeId, employee); //TODO - fix uglyness in Location route
            }

            return BadRequest(ModelState);
        }

        //TODO - Authentication strategy - Rules Admin or Employee as well - Custom attribute?
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Put(int id, [FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (_employees.Any(x => x.EmployeeId == id))
                {
                    employee.EmployeeId = id;
                    int index = _employees.IndexOf(_employees.First(x => x.EmployeeId == id));
                    _employees[index] = employee;
                    return StatusCode(HttpStatusCode.NoContent);
                }
                
                return NotFound();
            }

            return BadRequest(ModelState);
        }

        //TODO - Authentication strategy - Rules Admin or Employee as well - Custom attribute?
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Delete(int id)
        {
            Employee existingEmp = _employees.FirstOrDefault(x => x.EmployeeId == id);
            if (existingEmp != null)
            {
                _employees.Remove(existingEmp);
                return StatusCode(HttpStatusCode.NoContent);
            }

            return NotFound();
        }
    }
}
