using BlazorLaboratory.GraphQL.DataLoaders;
using BlazorLaboratory.GraphQL.Enums;
using BlazorLaboratory.GraphQL.Services;
using FirebaseAdmin.Auth;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    [IsProjected(true)] // always query for the Instructor id
    public Guid InstructorId { get; set; }
    //[GraphQLNonNullType]
    public async Task<InstructorType>? Instructor([Service] InstructorDataLoader instructorDataLoader)
    {
        var instructorDto = await instructorDataLoader.LoadAsync(InstructorId);
        //var instructorDto = await instructorRepository.GetById(InstructorId);
        return instructorDto.Adapt<InstructorType>();
    }
    public IEnumerable<StudentType>? Students { get; set; }

    //[IsProjected(false)] // never query for the NonExistingItemId id
	//public Guid NonExistingItemId { get; set; }

	public string Description()
    {
        return $"This is a {Name} description";
    }

    [IsProjected(true)]
    public Guid CreatorId { get; set; }

    public async Task<UserType?> Creator()
    {
        UserRecord? user = await FirebaseAuth.DefaultInstance.GetUserAsync("QIihfbEO4mbiyQlC7hAD53b0dHt2");
        return new UserType{ Email = user.Email, Id = Guid.Parse("9A6204D7-FB11-4FF6-826A-4967F98D7B91") };
    }
}
