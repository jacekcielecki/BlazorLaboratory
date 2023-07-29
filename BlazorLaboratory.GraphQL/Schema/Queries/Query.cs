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


    //These are the resolvers 
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
