using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Thoughts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ThoughtsDbConnectionString"];
builder.Services.AddDbContext<ThoughtsDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
       {
	       options.SignIn.RequireConfirmedAccount = false;
	       options.SignIn.RequireConfirmedEmail = false;
	       options.SignIn.RequireConfirmedPhoneNumber = false;
       })
	.AddEntityFrameworkStores<ThoughtsDbContext>();

builder.Services.AddScoped<IThoughtsRepository, ThoughtsRepository>();


var signingSecurityKey = builder.Configuration["SigningKey"];
var signingKey = new SigningSymmetricKey(signingSecurityKey);
builder.Services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(jwtBearerOptions =>
	{
		jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
		{
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