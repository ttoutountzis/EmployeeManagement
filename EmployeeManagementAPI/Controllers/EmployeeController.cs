using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuditLogHelper _auditLogHelper;

        public EmployeeController(AppDbContext context, AuditLogHelper auditLogHelper)
        {
            _context = context;
            _auditLogHelper = auditLogHelper;
        }

        /// <summary>
        /// Retrieves all employees with optional sorting and search functionality.
        /// </summary>
        /// <param name="sortBy">Sort by 'surname' or 'hireDate'.</param>
        /// <param name="search">Search by employee's first or last name.</param>
        /// <param name="skillId">Search by employee's skill by skill_id</param>
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? sortBy, [FromQuery] string? search, [FromQuery] int? skillId)
        {
            var query = _context.Employees
                .Include(e => e.Skills)
                .ThenInclude(es => es.Skill)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.FirstName.Contains(search) || e.LastName.Contains(search));
            }

            if (skillId.HasValue)
            {
                query = query.Where(e => e.Skills.Any(es => es.SkillId == skillId));
            }

            if (sortBy == "surname")
                query = query.OrderBy(e => e.LastName);
            else if (sortBy == "hireDate")
                query = query.OrderBy(e => e.HireDate);

            var employees = await query
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    HireDate = e.HireDate,
                    Skills = e.Skills.Select(es => new SkillDTO
                    {
                        Id = es.Skill.Id,
                        Name = es.Skill.Name,
                        Description = es.Skill.Description
                    }).ToList()
                })
                .ToListAsync();

            return Ok(employees);
        }


        /// <summary>
        /// Retrieves a specific employee by ID along with their skills.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Skills)
                .ThenInclude(es => es.Skill)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    HireDate = e.HireDate,
                    Skills = e.Skills.Select(es => new SkillDTO
                    {
                        Id = es.Skill.Id,
                        Name = es.Skill.Name,
                        Description = es.Skill.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest();

            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                HireDate = employeeDto.HireDate,
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            if (employeeDto.SkillIds != null && employeeDto.SkillIds.Any())
            {
                foreach (var skillId in employeeDto.SkillIds)
                {
                    _context.EmployeeSkills.Add(new EmployeeSkill
                    {
                        EmployeeId = employee.Id,
                        SkillId = skillId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeDto)
        {
            if (id != employeeDto.Id)
                return BadRequest();

            var employee = await _context.Employees
                .Include(e => e.Skills) 
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.HireDate = employeeDto.HireDate;

            var currentSkillIds = employee.Skills.Select(s => s.SkillId).ToList();
            var newSkillIds = employeeDto.SkillIds ?? new List<int>();

            var skillsToRemove = employee.Skills.Where(es => !newSkillIds.Contains(es.SkillId)).ToList();
            foreach (var skill in skillsToRemove)
            {
                _context.EmployeeSkills.Remove(skill);
            }

            var skillsToAdd = newSkillIds.Except(currentSkillIds);
            foreach (var skillId in skillsToAdd)
            {
                _context.EmployeeSkills.Add(new EmployeeSkill
                {
                    EmployeeId = id,
                    SkillId = skillId
                });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            await _auditLogHelper.LogAudit("Employee", "Delete", employee);
            return NoContent();
        }

        /// <summary>
        /// Imports employees from a CSV file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportEmployeesWithSkills([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            using var stream = new StreamReader(file.OpenReadStream());
            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 4)
                    continue;

                var firstName = parts[0];
                var lastName = parts[1];
                var hireDate = DateTime.Parse(parts[2]);
                var skillNames = parts[3].Split(';', StringSplitOptions.RemoveEmptyEntries);

                var employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    HireDate = hireDate
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                foreach (var skillName in skillNames)
                {
                    var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skillName.Trim());
                    if (skill == null)
                    {
                        skill = new Skill
                        {
                            Name = skillName.Trim(),
                            Description = $"Imported skill: {skillName}",
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.Skills.Add(skill);
                        await _context.SaveChangesAsync();
                    }

                    var employeeSkill = new EmployeeSkill
                    {
                        EmployeeId = employee.Id,
                        SkillId = skill.Id
                    };
                    _context.EmployeeSkills.Add(employeeSkill);
                }

                await _context.SaveChangesAsync();
            }

            return Ok("Employees and their skills imported successfully");
        }
    }
}
