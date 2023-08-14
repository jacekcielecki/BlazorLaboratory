using BlazorLaboratory.Shared.Enums;

namespace BlazorLaboratory.GraphQL.Schema.Mutations.Course;

public class CourseInputType
{
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}
