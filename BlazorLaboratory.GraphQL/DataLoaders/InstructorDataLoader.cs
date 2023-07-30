using BlazorLaboratory.GraphQL.Dto;
using BlazorLaboratory.GraphQL.Services;

namespace BlazorLaboratory.GraphQL.DataLoaders;

public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDto>
{
    private readonly InstructorRepository _instructorRepository;

    public InstructorDataLoader(
        InstructorRepository instructorRepository, 
        IBatchScheduler batchScheduler, 
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _instructorRepository = instructorRepository;
    }

    protected override async Task<IReadOnlyDictionary<Guid, InstructorDto>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var instructors = await _instructorRepository.GetManyByIds(keys);
        return instructors.ToDictionary(x => x.Id);
    }
}
