using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Talabat.Core.Services;

namespace Talabat.PL.Helper
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _expireTime;

		public CachedAttribute(int ExpireTime )
		{
			_expireTime = ExpireTime;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var CachedService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
			string CacheKey = GenerateCachKey(context.HttpContext.Request);
			var CacheResponse = await CachedService.GetCachedAsync(CacheKey); // Get Data From Cache

			if (!string.IsNullOrEmpty(CacheResponse))
			{
				var contentResult = new ContentResult()
				{
					Content = CacheResponse,
					ContentType = "application/json",
					StatusCode = 200
				};

				context.Result = contentResult;
				return;
			}

			var ActionExecutedResult = await next.Invoke();
			if (ActionExecutedResult.Result is OkObjectResult result)
				await CachedService.CacheDataAsync(CacheKey, result, TimeSpan.FromSeconds(_expireTime));

		}


		// Generate Specific Unique Key
		private string GenerateCachKey(HttpRequest request)
		{
			var CachKey = new StringBuilder();	//		/api/(EndPoint)
			CachKey.Append(request.Path);       //		/api/product

			foreach (var (key, value) in request.Query.OrderBy(k => k.Key)) //		/api/product|sort-name|pageindex-5|......
			{
				CachKey.Append($"|{key}-{value}");
			}
			return CachKey.ToString();
		}
	}
}
