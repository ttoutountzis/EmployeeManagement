using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Helpers;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class SkillsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuditLogHelper _auditLogHelper;

        public SkillsController(AppDbContext context, AuditLogHelper auditLogHelper)
        {
            _context = context;
            _auditLogHelper = auditLogHelper;
        }

        /// <summary>
        /// Retrieves all skills with employee count.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _context.Skills
                .Select(s => new SkillDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                })
                .ToListAsync();

            return Ok(skills);
        }

        /// <summary>
        /// Retrieves a specific skill by ID, including associated employees.
        /// </summary>
        /// <param name="id">Skill ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            var skill = await _context.Skills
                .Where(s => s.Id == id)
                .Select(s => new SkillDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                })
                .FirstOrDefaultAsync();

            if (skill == null)
                return NotFound();

            return Ok(skill);
        }

        /// <summary>
        /// Creates a new skill.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillDTO skillDTO)
        {
            var skill = new Skill
            {
                Name = skillDTO.Name,
                Description = skillDTO.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            await _auditLogHelper.LogAudit("Employee", "Create", skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skillDTO);
        }

        /// <summary>
        /// Updates an existing skill.
        /// </summary>
        /// <param name="id">Skill ID.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillDTO updatedSkill)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound();

            skill.Name = updatedSkill.Name;
            skill.Description = updatedSkill.Description;

            await _context.SaveChangesAsync();
            await _auditLogHelper.LogAudit("Employee", "Update", skill);
            return NoContent();
        }

        /// <summary>
        /// Deletes a skill by ID.
        /// </summary>
        /// <param name="id">Skill ID.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound();

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            await _auditLogHelper.LogAudit("Employee", "Delete", skill);
            return NoContent();
        }

        /// <summary>
        /// Exports all skills to a CSV file.
        /// </summary>
        /// <returns></returns>
        [HttpGet("export")]
        [Produces("text/csv")]
        public IActionResult ExportSkillsToCsv()
        {
            var skills = _context.Skills.ToList();

            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,CreatedAt");

            foreach (var skill in skills)
            {
                csv.AppendLine($"{skill.Id},{skill.Name},{skill.Description},{skill.CreatedAt:O}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "skills.csv");
        }

    }
}
