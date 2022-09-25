namespace Thoughts.Dto;

public class ThoughtDto
{
	public int Id { get; }
	public string Text { get; }
	public DateTime CreateTime { get; }
	
	public ThoughtDto(int id, string text, DateTime createTime)
	{
		Id = id;
		Text = text;
		CreateTime = createTime;
	}
}