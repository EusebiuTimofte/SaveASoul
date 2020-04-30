using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Contexts;
using Save_A_Soul.DTOs;
using Save_A_Soul.Models;
using SaveASoul.Cors;

namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeesController : ControllerBase
    {
        private readonly Context _context;

        public EmployeesController(Context context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            List<EmployeeDTO> dto = new List<EmployeeDTO>();
            
            foreach(Employee emp in employees)
            {
                dto.Add(new EmployeeDTO
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    BirthDate = emp.BirthDate,
                    Salary = emp.Salary,
                    JobType = emp.JobType,
                    Position = emp.Position,
                    ShelterId = (emp.Shelter != null) ? emp.Shelter.Id : 0
                });
            }

            return new JsonResult(dto);

        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeDTO emp = new EmployeeDTO {

                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Salary = employee.Salary,
                JobType = employee.JobType,
                Position = employee.Position,
                ShelterId = (employee.Shelter != null) ? employee.Shelter.Id : 0
            };

            return new JsonResult(emp);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            Employee emp = new Employee
            { 
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Salary = dto.Salary,
                JobType = dto.JobType,
                Position = dto.Position,
                Shelter = _context.Shelters.Find(dto.ShelterId)
            };

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostEmployee(EmployeeDTO dto)
        {
            Employee emp = new Employee
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Salary = dto.Salary,
                JobType = dto.JobType,
                Position = dto.Position,
                Shelter = _context.Shelters.Find(dto.ShelterId)
            };

            _context.Employees.Add(emp);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(emp.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            dto.Id = emp.Id;
            return new JsonResult(dto);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
