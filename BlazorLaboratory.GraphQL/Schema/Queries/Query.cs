using BlazorLaboratory.GraphQL.Schema.Queries;

namespace BlazorLaboratory.GraphQL.Schema;

public class Query
{
    //These are the resolvers 
    public IEnumerable<CourseType> GetCourses()
    {
        var courses = DataFakers.CourseFaker.Generate(5);
        return courses;
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        var course = DataFakers.CourseFaker.Generate(1).First();
        course.Id = id;

        return course;
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Test";
}
