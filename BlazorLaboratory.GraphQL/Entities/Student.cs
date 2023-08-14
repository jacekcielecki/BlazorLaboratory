namespace BlazorLaboratory.GraphQL.Dto;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }
    public IEnumerable<Course> Courses { get; set; }
}
