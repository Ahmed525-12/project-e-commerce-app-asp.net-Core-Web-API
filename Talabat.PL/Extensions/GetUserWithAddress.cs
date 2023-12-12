using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace Talabat.PL.Extensions
{
	public static class GetUserWithAddress
	{
		public static async Task<AppUser> GetUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(U => U.Email == Email);

			return user!;
		}
	}
}
