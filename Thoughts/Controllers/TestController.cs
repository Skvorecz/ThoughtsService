using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Thoughts.Controllers;

[ApiController]
[Route("test")]
[Authorize]
public class TestController : ControllerBase
{
	[HttpGet]
	public IActionResult Test()
	{
		var claims = HttpContext.User.Claims.Select(c => new[]{c.Type, c.Value});
		return Ok(claims.ToList());
	}
}