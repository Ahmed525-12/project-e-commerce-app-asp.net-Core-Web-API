using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public  string CreateTokenAsync(AppUser user)
		{
			// Claims
			var UserClaim = new List<Claim>()
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.DisplayNams)
			};

			// Security Key
			var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			// Create Token Object
			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Expire"])),
				claims: UserClaim,
				signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
				);

			return  new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
