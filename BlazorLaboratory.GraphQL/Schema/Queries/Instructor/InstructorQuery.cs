using BlazorLaboratory.GraphQL.Services;
using Mapster;

namespace BlazorLaboratory.GraphQL.Schema.Queries.Instructor;

[ExtendObjectType(typeof(Query))]
public class InstructorQuery
{
    private readonly InstructorRepository _instructorRepository;

    public InstructorQuery(InstructorRepository instructorRepository)
    {
        _instructorRepository = instructorRepository;
    }

    public async Task<IEnumerable<InstructorType>> GetInstructors()
    {
        var instructors = await _instructorRepository.Get();
        return instructors.Adapt<IEnumerable<InstructorType>>();
    }
}
