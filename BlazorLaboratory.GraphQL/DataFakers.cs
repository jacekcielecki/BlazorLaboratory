using BlazorLaboratory.GraphQL.Enums;
using BlazorLaboratory.GraphQL.Schema;
using BlazorLaboratory.GraphQL.Schema.Queries.Course;
using BlazorLaboratory.GraphQL.Schema.Queries.Instructor;
using BlazorLaboratory.GraphQL.Schema.Queries.Student;
using Bogus;

namespace BlazorLaboratory.GraphQL;

public static class DataFakers
{
    public static readonly Faker<InstructorType> InstructorFaker = new Faker<InstructorType>()
        .RuleFor(p => p.Id, f => Guid.NewGuid())
        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
        .RuleFor(p => p.LastName, f => f.Person.LastName)
        .RuleFor(p => p.Salary, f => f.Random.Double(0, 10000));

    public static readonly Faker<StudentType> StudentFaker = new Faker<StudentType>()
        .RuleFor(p => p.Id, f => Guid.NewGuid())
        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
        .RuleFor(p => p.LastName, f => f.Person.LastName)
        .RuleFor(p => p.GPA, f => f.Random.Double(1, 4));

    public static readonly Faker<CourseType> CourseFaker = new Faker<CourseType>()
        .RuleFor(p => p.Id, f => Guid.NewGuid())
        .RuleFor(p => p.Name, f => f.Name.JobTitle())
        .RuleFor(p => p.Subject, f => f.PickRandom<Subject>())
        .RuleFor(p => p.InstructorId, f => Guid.NewGuid())
        .RuleFor(p => p.Students, f => StudentFaker.Generate(5));
}
