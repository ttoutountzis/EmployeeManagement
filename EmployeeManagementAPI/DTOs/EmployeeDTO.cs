﻿namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public List<SkillDTO> Skills { get; set; }
    }
}
