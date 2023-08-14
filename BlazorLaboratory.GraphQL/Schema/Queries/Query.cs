using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Schema.Queries.Course;
using BlazorLaboratory.GraphQL.Schema.Queries.Instructor;
using BlazorLaboratory.GraphQL.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BlazorLaboratory.GraphQL.Schema.Queries;

public class Query
{
    [UseDbContext(typeof(SchoolDbContext))]
    public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
    {
        IEnumerable<Dto.Course> courseDtos = await context.Courses
            .Where(c => c.Name.Contains(term))
            .ToListAsync();
        IEnumerable<Dto.Instructor> instructorDtos = await context.Instructors
            .Where(c => c.FirstName.Contains(term) || c.LastName.Contains(term))
            .ToListAsync();

        var courses = courseDtos.Adapt<IEnumerable<CourseType>>();
        var instructors = instructorDtos.Adapt<IEnumerable<InstructorType>>();

        return new List<ISearchResultType>()
            .Concat(courses)
            .Concat(instructors);
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Test";
}
