using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Thoughts.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private const int ExpirationDays = 1;
	private readonly UserManager<IdentityUser> userManager;
	private readonly SignInManager<IdentityUser> signInManager;
	private readonly IUserStore<IdentityUser> store;
	private readonly IJwtSigningEncodingKey signingEncodingKey;

	public AuthenticationController(IJwtSigningEncodingKey signingEncodingKey,
	                                UserManager<IdentityUser> userManager,
	                                SignInManager<IdentityUser> signInManager,
	                                IUserStore<IdentityUser> store)
	{
		this.signingEncodingKey = signingEncodingKey;
		this.userManager = userManager;
		this.signInManager = signInManager;
		this.store = store;
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Authenticate(AuthenticationRequest request)
	{
		var result = await signInManager.PasswordSignInAsync(request.Name,
		                                                     request.Password,
		                                                     false,
		                                                     false);
		if (!result.Succeeded)
		{
			return Unauthorized();
		}

		var user = await store.FindByNameAsync(request.Name, new CancellationToken());

		var claims = new[]
		             {
			             new Claim(ClaimTypes.NameIdentifier, user.Id)
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