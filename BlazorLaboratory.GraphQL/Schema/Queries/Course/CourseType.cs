using BlazorLaboratory.GraphQL.DataLoaders;
using BlazorLaboratory.GraphQL.Schema.Queries.FirebaseUser;
using BlazorLaboratory.GraphQL.Schema.Queries.Instructor;
using BlazorLaboratory.GraphQL.Schema.Queries.Student;
using BlazorLaboratory.Shared.Enums;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace BlazorLaboratory.GraphQL.Schema.Queries.Course;

public class CourseType : ISearchResultType
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

    //[IsProjected(false)] // never query for the SomeRelatedItemId id
    //public Guid SomeRelatedItemId { get; set; }

    public string Description()
    {
        return $"This is a {Name} description";
    }

    [IsProjected(true)]
    public string? CreatorId { get; set; }

    public async Task<FirebaseUserType?> Creator([Service] FirebaseUserDataLoader userDataLoader)
    {
        if (CreatorId.IsNullOrEmpty())
        {
            return null;
        }

        //UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(CreatorId);
        //return new UserType { Username = user.Email, Id = CreatorId };
        return await userDataLoader.LoadAsync(CreatorId, CancellationToken.None);
    }
}
