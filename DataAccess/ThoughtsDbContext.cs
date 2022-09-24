using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ThoughtsDbContext : IdentityDbContext
{
	public DbSet<Thought> Thoughts { get; set; }
	
	public ThoughtsDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Thought>(b =>
		{
			b.HasKey(t => t.Id);
			b.Property(t => t.Text).IsRequired();
			b.Property(t => t.CreateTime).IsRequired();
			b.Property(t => t.AuthorId).IsRequired();
		});
	}
}