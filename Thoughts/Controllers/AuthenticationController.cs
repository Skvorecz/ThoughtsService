using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Thoughts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private const int ExpirationDays = 1;
	
	[AllowAnonymous]
	[HttpPost]
	public ActionResult<string> Post(
		AuthenticationRequest authRequest,
		[FromServices] IJwtSigningEncodingKey signingEncodingKey)
	{
		//todo check request data
		
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
		return jwtToken;
	}
}