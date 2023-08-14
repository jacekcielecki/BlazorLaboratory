namespace BlazorLaboratory.GraphQL.Dto;

public class Instructor
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double? Salary { get; set; }
    public IEnumerable<Course> Courses { get; set; }
}
