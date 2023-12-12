namespace Talabat.PL.Errors
{
	public class ApiExceptionResponse : ApiResponse
	{
        public string? Details { get; set; }

        public ApiExceptionResponse(int StatusCode, string? Message = null, string? details = null ):base(StatusCode,Message)
        {
            Details = details;
        }
    }
}
