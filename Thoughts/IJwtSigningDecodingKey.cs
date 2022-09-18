using Microsoft.IdentityModel.Tokens;

namespace Thoughts;

public interface IJwtSigningDecodingKey
{
	SecurityKey GetKey();
}