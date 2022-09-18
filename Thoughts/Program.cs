using Microsoft.IdentityModel.Tokens;
using Thoughts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string signingSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
var signingKey = new SigningSymmetricKey(signingSecurityKey);
builder.Services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

const string jwtSchemeName = "JwtBearer";
builder.Services
	.AddAuthentication(options => {
		options.DefaultAuthenticateScheme = jwtSchemeName;
		options.DefaultChallengeScheme = jwtSchemeName;
	})
	.AddJwtBearer(jwtSchemeName, jwtBearerOptions => {
		jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters() {
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = signingKey.GetKey(),
 
			ValidateIssuer = true,
			ValidIssuer = "ThoughtsService",
 
			ValidateAudience = true,
			ValidAudience = "ThoughtsClient",
 
			ValidateLifetime = true,
 
			ClockSkew = TimeSpan.FromSeconds(5)
		};
	});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();