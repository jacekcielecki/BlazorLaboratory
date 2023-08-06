namespace BlazorLaboratory.GraphQL.Schema.Queries.Instructor;

public class InstructorType : ISearchResultType
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double? Salary { get; set; }
}
