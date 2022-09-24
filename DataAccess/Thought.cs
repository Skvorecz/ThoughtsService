using Microsoft.AspNetCore.Identity;

namespace DataAccess;

public class Thought
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime CreateTime { get; set; }
	
	
	public string AuthorId { get; set; }
	public IdentityUser Author { get; set; }
}