using BlazorLaboratory.Shared.Enums;

namespace BlazorLaboratory.Shared.DTOs;

public class CourseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Subject? Subject { get; set; }
    public Guid? InstructorId { get; set; }
    public InstructorDto? Instructor { get; set; }
    public IEnumerable<StudentType>? Students { get; set; }
    public string? Description { get; set; }
    public string? CreatorId { get; set; }
    public FirebaseUserDto? Creator { get; set; }
}
