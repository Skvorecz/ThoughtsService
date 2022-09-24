using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thoughts.Dto;

namespace Thoughts.Controllers;

[Authorize]
[Route("api/thoughts")]
[ApiController]
public class ThoughtsController : ControllerBase
{
	private readonly IThoughtsService thoughtsService;
	private readonly UserManager<IdentityUser> userManager;

	public ThoughtsController(UserManager<IdentityUser> userManager, IThoughtsService thoughtsService)
	{
		this.userManager = userManager;
		this.thoughtsService = thoughtsService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody]string text)
	{
		var author = await GetUserAsync();
		await thoughtsService.CreateThoughtAsync(text, author);
		
		return Ok();
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var user = await GetUserAsync();

		try
		{
			var thought = await thoughtsService.GetThoughtByIdAsync(id, user);
			var dto = new ThoughtDto(thought.Id, thought.Text, thought.CreateTime);
			return Ok(dto);
		}
		catch (ArgumentException)
		{
			return Unauthorized();
		}
	}

	[HttpGet("all")]
	public async Task<IActionResult> GetAllAsync()
	{
		var user = await GetUserAsync();
		var thoughts = (await thoughtsService.GetAllUserThoughtsAsync(user))
		                                    .Select(t=>new ThoughtDto(t.Id, t.Text, t.CreateTime));
		return Ok(thoughts);
	}

	private async Task<IdentityUser> GetUserAsync()
	{
		return await userManager.GetUserAsync(User);
	}
}