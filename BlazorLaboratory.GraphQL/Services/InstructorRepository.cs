using BlazorLaboratory.GraphQL.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlazorLaboratory.GraphQL.Services;

public class InstructorRepository
{
    private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

    public InstructorRepository(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<InstructorDto?> GetById(Guid id)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Instructors
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<InstructorDto>> GetManyByIds(IReadOnlyList<Guid> keys)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Instructors
            .Where(x => keys.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<InstructorDto>> Get()
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        return await db.Instructors.ToListAsync();
    }

	public async Task<InstructorDto> Create(InstructorDto instructor)
    {
        await using SchoolDbContext db = await _contextFactory.CreateDbContextAsync();
        db.Instructors.Add(instructor);
        await db.SaveChangesAsync();

        return instructor;
	}
}