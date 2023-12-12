using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository;
using Talabat.PL.Errors;
using Talabat.PL.Extensions;
using Talabat.PL.Helper;
using Talabat.PL.MiddleWare;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.PL
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Service Configuration

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("AppIdentityConnection"));
			});

			builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
			{
				var connection = builder.Configuration.GetConnectionString("RidusConnection");

				return ConnectionMultiplexer.Connect(connection);
			});

			builder.Services.AddAplicationServices();

			builder.Services.AddIdentityServices(builder.Configuration);

			#endregion

			var app = builder.Build();

			using var scope = app.Services.CreateScope();

			var service = scope.ServiceProvider;

			var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

			try
			{
				var DbContext = service.GetRequiredService<StoreContext>();

				await DbContext.Database.MigrateAsync();

				var IdentityDbContext = service.GetRequiredService<AppIdentityDbContext>();

				var userManager = service.GetRequiredService<UserManager<AppUser>>();

				await IdentityDbContext.Database.MigrateAsync();

				await AppIdentityDbContextSeed.SeedUserAsync(userManager);

				await DataStoreSeed.SeedAsync(DbContext);
			}
			catch (Exception ex)
			{

				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "Error during update Database");
			}

			// Configure the HTTP request pipeline.
			#region Configure Kestrel

			app.UseMiddleware<ExceptionMiddleWare>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers(); 

			#endregion

			app.Run();
		}
	}
}