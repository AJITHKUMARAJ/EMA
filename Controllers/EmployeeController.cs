using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EMA.Data;
using EMA.Models;
using EMA.Models.Entities;

namespace EMA.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public EmployeeController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Get a single employee by id
        /// </summary>
        [HttpGet("GetEmployeeById/{id:guid}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            employee.Id = Guid.NewGuid();

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto dto)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _mapper.Map(dto, employee);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        [HttpDelete("DeleteEmployee/{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}