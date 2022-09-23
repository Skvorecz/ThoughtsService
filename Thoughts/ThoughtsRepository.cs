using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Thoughts;

public class ThoughtsRepository : IThoughtsRepository
{
	private readonly ThoughtsDbContext dbContext;

	public ThoughtsRepository(ThoughtsDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task Create(string text, DateTime createTime, IdentityUser author)
	{
		var thought = new Thought()
		{
			Text = text,
			CreateTime = createTime,
			Author = author
		};

		await dbContext.Thoughts.AddAsync(thought);
	}
	
	public async Task<Thought> GetThoughtById(int id)
	{
		return await dbContext.Thoughts.FirstAsync(t => t.Id == id);
	}

	public async Task<List<Thought>> GetAllUserThoughts(IdentityUser author)
	{
		return await dbContext.Thoughts
			.Where(t => t.Author == author)
			.ToListAsync();
	}
}