using Microsoft.IdentityModel.Tokens;

namespace Thoughts;

public interface IJwtSigningEncodingKey
{
	string SigningAlgorithm { get; }
 
	SecurityKey GetKey();
}