using BlazorLaboratory.GraphQL.Schema.Queries;
using BlazorLaboratory.GraphQL.Services;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema;

public class Query
{
    private readonly CoursesRepository _coursesRepository;

    public Query(CoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    [UseDbContext(typeof(SchoolDbContext))]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 5)]
    public IQueryable<CourseType> GetOffsetCourses([ScopedService] SchoolDbContext context)
    {
        return context.Courses.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId,
        });
    }

    [UseDbContext(typeof(SchoolDbContext))]
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 5)]
    public IQueryable<CourseType> GetPagedCourses([ScopedService] SchoolDbContext context)
    {
        return context.Courses.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId

        });
    }

    public async Task<IEnumerable<CourseType>> GetCourses()
    {
        var courses = await _coursesRepository.Get();
        return courses.Adapt<IEnumerable<CourseType>>();
    }

    public async Task<CourseType> GetCourseById(Guid id)
    {
        var course = await _coursesRepository.GetById(id);
        return course.Adapt<CourseType>();
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Test";
}
