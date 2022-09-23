using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Thoughts.Controllers;

[Authorize]
[Route("api/thoughts")]
[ApiController]
public class ThoughtsController : ControllerBase
{
	private readonly IThoughtsRepository repository;
	private readonly UserManager<IdentityUser> userManager;

	public ThoughtsController(IThoughtsRepository repository,
		UserManager<IdentityUser> userManager)
	{
		this.repository = repository;
		this.userManager = userManager;
	}

	[HttpPost]
	public async Task<IActionResult> Create(string text)
	{
		await repository.Create(text,
		                        DateTime.UtcNow,
		                        await GetUser());
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> Get(int id)
	{
		var user = await GetUser();
		var thought = await repository.GetThoughtById(id);
		return thought.Author == user
			       ? Ok(thought)
			       : Unauthorized();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var thoughts = repository.GetAllUserThoughts(await GetUser());
		return Ok(thoughts);
	}

	private async Task<IdentityUser> GetUser()
	{
		return await userManager.GetUserAsync(User);
	}
}