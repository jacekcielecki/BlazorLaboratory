using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Filter;
using BlazorLaboratory.GraphQL.Schema.Sorters;
using BlazorLaboratory.GraphQL.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BlazorLaboratory.GraphQL.Schema.Queries;

public class Query
{
    private readonly CoursesRepository _coursesRepository;

    public Query(CoursesRepository coursesRepository)
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

    [UseDbContext(typeof(SchoolDbContext))]
    public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
    {
        IEnumerable<CourseDto> courseDtos = await context.Courses
            .Where(c => c.Name.Contains(term))
            .ToListAsync();
        IEnumerable<InstructorDto> instructorDtos = await context.Instructors
            .Where(c => c.FirstName.Contains(term) || c.LastName.Contains(term))
            .ToListAsync();

        var courses = courseDtos.Adapt<IEnumerable<CourseType>>();
        var instructors = instructorDtos.Adapt<IEnumerable<InstructorType>>();

        return new List<ISearchResultType>()
            .Concat(courses)
            .Concat(instructors);
    }

	public async Task<CourseType> GetCourseById(Guid id)
    {
        var course = await _coursesRepository.GetById(id);
        return course.Adapt<CourseType>();
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Test";
}
