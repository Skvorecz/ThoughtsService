using DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Application;

public class ThoughtsService : IThoughtsService
{
	private readonly IThoughtsRepository repository;

	public ThoughtsService(IThoughtsRepository repository)
	{
		this.repository = repository;
	}

	public async Task CreateThoughtAsync(string text, IdentityUser author)
	{
		if (author == null)
		{
			throw new ArgumentNullException(nameof(author));
		}

		var thought = new Thought
		              {
			              Text = text,
			              CreateTime = DateTime.UtcNow,
			              Author = author
		              };

		await repository.CreateThought(thought);
	}

	public async Task<Thought> GetThoughtByIdAsync(int id, IdentityUser author)
	{
		var thought = await repository.GetThoughtById(id);

		if (thought.AuthorId != author.Id)
		{
			throw new ArgumentException(null, nameof(id));
		}

		return thought;
	}

	public async Task<List<Thought>> GetAllUserThoughtsAsync(IdentityUser author)
	{
		return await repository.GetAllUserThoughts(author.Id);
	}
}