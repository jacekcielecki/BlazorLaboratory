using BlazorLaboratory.GraphQL.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorLaboratory.GraphQL.Services;

public class SchoolDbContext : DbContext
{
    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<InstructorDto> Instructors { get; set; }
    public DbSet<StudentDto> Students { get; set; }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }
}
