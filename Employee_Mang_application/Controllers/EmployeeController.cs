using Employee_Mang_application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Employee_Mang_application.Models;
using Employee_Mang_application.Models.Entity;
using Employee_Mang_application.Service;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Employee_Mang_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        // private readonly EmployeeService _employeeService;



        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            // _employeeService = service;
        }




        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            //var allemployees = dbContext.Employees.ToList();
            return Ok(dbContext.Employees.ToList());

        }


        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);

        }


        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto employeeDto)
        {
            if (string.IsNullOrEmpty(employeeDto.FirstName))
            {
                throw new ArgumentNullException(nameof(employeeDto.FirstName), "FirstName cannot be null");
            }
            if (string.IsNullOrEmpty(employeeDto.LastName))
            {
                throw new ArgumentNullException(nameof(employeeDto.FirstName), "LastName cannot be null");
            }
            if (string.IsNullOrEmpty(employeeDto.Email))
            {
                throw new ArgumentNullException(nameof(employeeDto.Email), "Email cannot be null or empty");
            }






            var employeeEntity = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
                DateOfJoining = employeeDto.DateOfJoining,
            };



            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (string.IsNullOrEmpty(updateEmployeeDto.FirstName))
            {
                throw new ArgumentNullException(nameof(updateEmployeeDto.FirstName), "FirstName cannot be null");
            }
            if (string.IsNullOrEmpty(updateEmployeeDto.LastName))
            {
                throw new ArgumentNullException(nameof(updateEmployeeDto.FirstName), "LastName cannot be null");
            }
            if (string.IsNullOrEmpty(updateEmployeeDto.Email))
            {
                throw new ArgumentNullException(nameof(updateEmployeeDto.Email), "Email cannot be null or empty");
            }

            employee.FirstName = updateEmployeeDto.FirstName;
            employee.LastName = updateEmployeeDto.LastName;
            employee.Email = updateEmployeeDto.Email;
            employee.DateOfJoining = updateEmployeeDto.DateOfJoining;
            employee.Department = updateEmployeeDto.Department;


            dbContext.SaveChanges();
            return Ok(employee);


        }


        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok("record deleted succesfully");
        }

        [HttpGet("search")]
       
        public IActionResult SearchEmployees([FromQuery] string name)
        {
            IQueryable<Employee> query = dbContext.Employees;

            // Filter by name if provided
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name));
            }
            return Ok(query);
        }

      

        [HttpGet("searchApi")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchAPi([FromQuery] string FirstName, [FromQuery] string LastName,
            [FromQuery] string departmentName,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var query = dbContext.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(FirstName))
            {
                query = query.Where(e => e.FirstName.Contains(FirstName));
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                query = query.Where(e => e.LastName.Contains(LastName));
            }

            if (!string.IsNullOrEmpty(departmentName))
            {
                query = query.Where(e => e.Department.Contains(departmentName));
            }

            if (fromDate.HasValue)
            {
                query = query.Where(e => e.DateOfJoining >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.DateOfJoining <= toDate.Value);
            }

            return await query.ToListAsync();
        }




    }
    }
