using BlazorLaboratory.GraphQL.Enums;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class CourseInputType
{
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}
