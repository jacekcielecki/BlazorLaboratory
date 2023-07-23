using BlazorLaboratory.GraphQL.Enums;

namespace BlazorLaboratory.GraphQL.Schema;

public class CourseType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    [GraphQLNonNullType]
    public InstructorType Instructor { get; set; }
    public IEnumerable<StudentType> Students { get; set; }

    public string Description()
    {
        return $"This is a {Name} description";
    }
}
