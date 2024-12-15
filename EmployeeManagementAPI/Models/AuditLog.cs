namespace EmployeeManagementAPI.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityType { get; set; } 
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
