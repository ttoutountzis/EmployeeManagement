using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeManagementAPI.Helpers
{
    public class AuditLogHelper
    {
        private readonly AppDbContext _context;
        public AuditLogHelper(AppDbContext context)
        {
            _context = context;
        }
        public async Task LogAudit(string entityType, string action, object details)
        {
            var log = new AuditLog
            {
                EntityType = entityType,
                Action = action,
                Details = JsonSerializer.Serialize(details),
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

    }
}
