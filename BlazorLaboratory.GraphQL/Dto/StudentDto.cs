namespace BlazorLaboratory.GraphQL.Dto;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; }
}
