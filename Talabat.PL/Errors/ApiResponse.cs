namespace Talabat.PL.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message??GetDefaultMsgForStatusCode(statusCode);
        }

		private string? GetDefaultMsgForStatusCode(int statusCode)
		{
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Not Authorized",
                404 => "Not Found",
                505 => "Internal Server Error",
                _ => null,
            };
		}
	}
}
