using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Thoughts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private const int ExpirationDays = 1;
	private readonly UserManager<IdentityUser> userManager;
	private readonly SignInManager<IdentityUser> signInManager;
	private readonly IJwtSigningEncodingKey signingEncodingKey;

	public AuthenticationController(IJwtSigningEncodingKey signingEncodingKey,
		UserManager<IdentityUser> userManager,
		SignInManager<IdentityUser> signInManager)
	{
		this.signingEncodingKey = signingEncodingKey;
		this.userManager = userManager;
		this.signInManager = signInManager;
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Authenticate(AuthenticationRequest authRequest)
	{
		var result = signInManager.PasswordSignInAsync(authRequest.Name,
																	authRequest.Password,
																	false,
																	false);
		if (!result.IsCompletedSuccessfully)
		{
			return Unauthorized();
		}

		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
		};

		var token = new JwtSecurityToken(
			"ThoughtsService",
			"ThoughtsClient",
			claims,
			expires: DateTime.Now.AddDays(ExpirationDays),
			signingCredentials: new SigningCredentials(
				signingEncodingKey.GetKey(),
				signingEncodingKey.SigningAlgorithm)
		);

		var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
		return Ok(jwtToken);
	}

	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterRequest request)
	{
		var user = new IdentityUser(request.Name) { Email = request.Email };
		var result = await userManager.CreateAsync(user, request.Password);
		return Ok(result);
	}
}