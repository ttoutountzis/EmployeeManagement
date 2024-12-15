namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public List<int>? SkillIds { get; set; }
    }

}
