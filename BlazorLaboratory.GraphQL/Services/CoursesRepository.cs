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
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Courses.ToListAsync();
    }

    public async Task<CourseDto?> GetById(Guid id)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Courses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CourseDto> Create(CourseDto course)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        db.Courses.Add(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<CourseDto> Update(CourseDto course)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        db.Courses.Update(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<bool> Delete(Guid id)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        CourseDto course = new CourseDto()
        {
            Id = id
        };

        db.Courses.Remove(course);
        return await db.SaveChangesAsync() > 0;
    }
}
