using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "HireDate", "LastName" },
                values: new object[,]
                {
                    { 1, "John", new DateTime(2024, 9, 6, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5814), "Doe" },
                    { 2, "Jane", new DateTime(2024, 5, 29, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5819), "Smith" },
                    { 3, "Michael", new DateTime(2024, 7, 18, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5820), "Brown" },
                    { 4, "Emily", new DateTime(2024, 10, 26, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5821), "Davis" },
                    { 5, "Chris", new DateTime(2024, 11, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5823), "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5713), "Expertise in C# development", "C# Programming" },
                    { 2, new DateTime(2024, 12, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5715), "Experience with SQL databases", "SQL Database" },
                    { 3, new DateTime(2024, 12, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5716), "Front-end expertise using React", "React Development" },
                    { 4, new DateTime(2024, 12, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5717), "Building scalable REST APIs", "API Design" },
                    { 5, new DateTime(2024, 12, 15, 6, 9, 21, 786, DateTimeKind.Utc).AddTicks(5718), "Strong problem-solving skills", "Problem Solving" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeSkills",
                columns: new[] { "EmployeeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 1 },
                    { 5, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "EmployeeSkills",
                keyColumns: new[] { "EmployeeId", "SkillId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
