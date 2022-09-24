using Microsoft.AspNetCore.Identity;

namespace DataAccess;

public interface IThoughtsRepository
{
	Task CreateThought(Thought thought);
	Task<Thought> GetThoughtById(int id);
	Task<List<Thought>> GetAllUserThoughts(string authorId);
}