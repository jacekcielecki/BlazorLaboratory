﻿namespace BlazorLaboratory.GraphQL.Schema;

public class InstructorType
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double? Salary { get; set; }
}
