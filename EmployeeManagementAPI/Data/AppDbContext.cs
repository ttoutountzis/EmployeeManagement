using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(es => new { es.EmployeeId, es.SkillId });

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.Skills)
                .HasForeignKey(es => es.EmployeeId);

            modelBuilder.Entity<EmployeeSkill>()
                .HasOne(es => es.Skill)
                .WithMany()
                .HasForeignKey(es => es.SkillId);

            modelBuilder.Entity<Skill>().HasData(
               new Skill { Id = 1, Name = "C# Programming", Description = "Expertise in C# development", CreatedAt = DateTime.UtcNow },
               new Skill { Id = 2, Name = "SQL Database", Description = "Experience with SQL databases", CreatedAt = DateTime.UtcNow },
               new Skill { Id = 3, Name = "React Development", Description = "Front-end expertise using React", CreatedAt = DateTime.UtcNow },
               new Skill { Id = 4, Name = "API Design", Description = "Building scalable REST APIs", CreatedAt = DateTime.UtcNow },
               new Skill { Id = 5, Name = "Problem Solving", Description = "Strong problem-solving skills", CreatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", HireDate = DateTime.UtcNow.AddDays(-100) },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", HireDate = DateTime.UtcNow.AddDays(-200) },
                new Employee { Id = 3, FirstName = "Michael", LastName = "Brown", HireDate = DateTime.UtcNow.AddDays(-150) },
                new Employee { Id = 4, FirstName = "Emily", LastName = "Davis", HireDate = DateTime.UtcNow.AddDays(-50) },
                new Employee { Id = 5, FirstName = "Chris", LastName = "Johnson", HireDate = DateTime.UtcNow.AddDays(-30) }
            );

            modelBuilder.Entity<EmployeeSkill>().HasData(
                new EmployeeSkill { EmployeeId = 1, SkillId = 1 },
                new EmployeeSkill { EmployeeId = 1, SkillId = 3 },
                new EmployeeSkill { EmployeeId = 2, SkillId = 2 },
                new EmployeeSkill { EmployeeId = 3, SkillId = 4 },
                new EmployeeSkill { EmployeeId = 4, SkillId = 5 },
                new EmployeeSkill { EmployeeId = 5, SkillId = 1 },
                new EmployeeSkill { EmployeeId = 5, SkillId = 2 }
            );
        }
    }
}
