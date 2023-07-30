using BlazorLaboratory.GraphQL.DataLoaders;
using BlazorLaboratory.GraphQL.Enums;
using BlazorLaboratory.GraphQL.Services;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
    //[GraphQLNonNullType]
    public async Task<InstructorType>? Instructor([Service] InstructorDataLoader instructorDataLoader)
    {
        var instructorDto = await instructorDataLoader.LoadAsync(InstructorId);
        //var instructorDto = await instructorRepository.GetById(InstructorId);
        return instructorDto.Adapt<InstructorType>();
    }
    public IEnumerable<StudentType>? Students { get; set; }

    public string Description()
    {
        return $"This is a {Name} description";
    }
}
