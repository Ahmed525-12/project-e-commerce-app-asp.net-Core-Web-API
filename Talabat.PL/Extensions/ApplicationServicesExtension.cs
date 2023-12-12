using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.PL.Errors;
using Talabat.PL.Helper;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.PL.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
		{

			Services.AddSingleton(typeof(ICacheService), typeof(CacheService));
			Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfwork));
			Services.AddScoped(typeof(IOrderService), typeof(OrderService));
			Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			//Services.AddScoped(typeof(IGenerecRepository<>), typeof(GenerecRepository<>));

			Services.AddAutoMapper(typeof(MappingProfiles));

			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(e => e.ErrorMessage)
														 .ToArray();
					var ValidationErrorResponse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(ValidationErrorResponse);
				};
			});

			return Services; 
		}
	}
}
