using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Thoughts;

public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
{
	private readonly SymmetricSecurityKey secretKey;
 
	public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;
 
	public SigningSymmetricKey(string key)
	{
		secretKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(key));
	}
 
	public SecurityKey GetKey() => secretKey;
}