using BlazorLaboratory.GraphQL.Filter;
using BlazorLaboratory.GraphQL.Schema.Sorters;
using BlazorLaboratory.GraphQL.Services;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Queries.Course;

[ExtendObjectType(typeof(Query))]
public class CourseQuery
{
    private readonly CoursesRepository _coursesRepository;

    public CourseQuery(CoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    [UseDbContext(typeof(SchoolDbContext))]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 5)]
    public IQueryable<CourseType> GetOffsetPagedCourses([ScopedService] SchoolDbContext context)
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
    [UseProjection]
    [UseFiltering(filterType: typeof(CourseFilterType))]
    [UseSorting(sortingType: typeof(CourseSortType))]
    public IQueryable<CourseType> GetPagedCourses([ScopedService] SchoolDbContext context)
    {
        return context.Courses.Select(x => new CourseType
        {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId,
            CreatorId = x.CreatorId
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
}
