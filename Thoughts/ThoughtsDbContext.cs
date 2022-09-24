using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Thoughts;

public class ThoughtsDbContext : IdentityDbContext
{
	public ThoughtsDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Thought> Thoughts { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		
		builder.Entity<Thought>()
		       .Property(t => t.Text).IsRequired();
		builder.Entity<Thought>()
		       .Property(t => t.CreateTime).IsRequired();
		builder.Entity<Thought>()
		       .Property(t => t.AuthorId).IsRequired();
		builder.Entity<Thought>()
		       .HasOne<IdentityUser>()
		       .WithMany()
		       .HasForeignKey(u => u.AuthorId)
		       .IsRequired();
	}
}