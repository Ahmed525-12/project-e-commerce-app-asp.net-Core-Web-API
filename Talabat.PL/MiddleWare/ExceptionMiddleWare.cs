using System.Net;
using System.Text.Json;
using Talabat.PL.Errors;

namespace Talabat.PL.MiddleWare
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleWare> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/josn";
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

				//if(_env.IsDevelopment())
				//{
				//	var Response = new ApiExceptionResponse(500,ex.Message, ex.StackTrace.ToString());
				//}
				//else
				//{
				//	var Respose = new ApiExceptionResponse(500);
				//}

				var Response = _env.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) 
												: new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString());
				var options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				var ResponseJson = JsonSerializer.Serialize(Response,options);
				context.Response.WriteAsync(ResponseJson);
			}
		}
    }
}
