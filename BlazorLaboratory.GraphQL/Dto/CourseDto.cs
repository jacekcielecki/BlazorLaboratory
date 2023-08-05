using BlazorLaboratory.GraphQL.Enums;
using BlazorLaboratory.GraphQL.Schema.Queries;
using BlazorLaboratory.GraphQL.Schema;

namespace BlazorLaboratory.GraphQL.Dto;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
    public InstructorDto Instructor { get; set; }
    public IEnumerable<StudentDto> Students { get; set; }
    public string? CreatorId { get; set; }
}
