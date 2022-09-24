using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ThoughtsRepository : IThoughtsRepository
{
	private readonly ThoughtsDbContext dbContext;

	public ThoughtsRepository(ThoughtsDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task CreateThought(Thought thought)
	{
		await dbContext.Thoughts.AddAsync(thought);
		await dbContext.SaveChangesAsync();
	}
	
	public async Task<Thought> GetThoughtById(int id)
	{
		return await dbContext.Thoughts.FirstAsync(t => t.Id == id);
	}

	public async Task<List<Thought>> GetAllUserThoughts(string authorId)
	{
		return await dbContext.Thoughts
			.Where(t => t.AuthorId == authorId)
			.ToListAsync();
	}
}