using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Thoughts;

public class ThoughtsDbContext : IdentityDbContext<IdentityUser>
{
	public ThoughtsDbContext(DbContextOptions options) : base(options)
	{
	}
}