using BlazorLaboratory.GraphQL.Enums;

namespace BlazorLaboratory.GraphQL.Schema.Mutations;

public class CourseResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}
