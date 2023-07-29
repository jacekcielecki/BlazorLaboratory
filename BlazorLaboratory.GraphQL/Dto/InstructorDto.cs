namespace BlazorLaboratory.GraphQL.Dto;

public class InstructorDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double? Salary { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; }
}
