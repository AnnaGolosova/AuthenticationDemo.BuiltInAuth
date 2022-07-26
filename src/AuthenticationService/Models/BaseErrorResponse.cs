namespace Demo.AuthService.Models
{
    public class BaseErrorResponse
    {
        public int StatusCode { get; }
        public string Message { get; }
        public string Details { get; }

        public BaseErrorResponse(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
