using BlazorLaboratory.GraphQL.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorLaboratory.GraphQL.Services;

public class CoursesRepository
{
    private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

    public CoursesRepository(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<CourseDto>> Get()
    {
        using SchoolDbContext db = _contextFactory.CreateDbContext();
        return await db.Courses
            .Include(x => x.Instructor)
            .Include(x => x.Students)
            .ToListAsync();
    }

    public async Task<CourseDto?> GetById(Guid id)
    {
        using SchoolDbContext db = _contextFactory.CreateDbContext();
        return await db.Courses
            .Include(x => x.Instructor)
            .Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CourseDto> Create(CourseDto course)
    {
        using SchoolDbContext db = _contextFactory.CreateDbContext();
        db.Courses.Add(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<CourseDto> Update(CourseDto course)
    {
        using SchoolDbContext db = _contextFactory.CreateDbContext();
        db.Courses.Update(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<bool> Delete(Guid id)
    {
        using SchoolDbContext db = _contextFactory.CreateDbContext();
        CourseDto course = new CourseDto()
        {
            Id = id
        };

        db.Courses.Remove(course);
        return await db.SaveChangesAsync() > 0;
    }
}
