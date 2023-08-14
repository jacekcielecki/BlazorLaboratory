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

    public async Task<IEnumerable<Course>> Get()
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Courses.ToListAsync();
    }

    public async Task<Course?> GetById(Guid id)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Courses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Course> Create(Course course)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        db.Courses.Add(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<Course> Update(Course course)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        db.Courses.Update(course);
        await db.SaveChangesAsync();

        return course;
    }

    public async Task<bool> Delete(Guid id)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        Course course = new Course()
        {
            Id = id
        };

        db.Courses.Remove(course);
        return await db.SaveChangesAsync() > 0;
    }
}
