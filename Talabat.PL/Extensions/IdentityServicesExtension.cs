using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.PL.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices (this IServiceCollection Services, IConfiguration _configuration)
		{
			Services.AddScoped<ITokenService, TokenService>();

			Services.AddIdentity<AppUser, IdentityRole>()
							.AddEntityFrameworkStores<AppIdentityDbContext>();

			Services.AddAuthentication(Option =>
			{
				Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			})
					.AddJwtBearer(option =>
					{
						option.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuer = true,
							ValidIssuer = _configuration["JWT:Issuer"],
							ValidateAudience = true,
							ValidAudience = _configuration["JWT:Audience"],
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
						};
					});

			return Services;
		}
	}
}
