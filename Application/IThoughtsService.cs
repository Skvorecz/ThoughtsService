using DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Application;

public interface IThoughtsService
{
	Task CreateThoughtAsync(string text, IdentityUser author);
	Task<Thought> GetThoughtByIdAsync(int id, IdentityUser author);
	Task<List<Thought>> GetAllUserThoughtsAsync(IdentityUser author);
}