using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Thoughts;

public class ThoughtsDbContext : IdentityDbContext
{
	public DbSet<Thought> Thoughts { get; set; }
	
	public ThoughtsDbContext(DbContextOptions options) : base(options)
	{
	}
}