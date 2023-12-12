using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayNams = "Ehab Ahmed",
					Email = "e.ahmed2684@gmail.com",
					UserName = "e.ahmed2684",
					PhoneNumber = "01021323989",
				};

				await userManager.CreateAsync(user, "P@ssw0rd");
			}

		}
	}
}
