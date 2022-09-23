using Microsoft.AspNetCore.Identity;

namespace Thoughts;

public interface IThoughtsRepository
{
	Task Create(string text, DateTime createTime, IdentityUser author);
	Task<Thought> GetThoughtById(int id);
	Task<List<Thought>> GetAllUserThoughts(IdentityUser author);
}