using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
	public class AppIdentityDbContext :IdentityDbContext<AppUser>
	{
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> Options):base(Options)
        {
            
        }
    }
}
