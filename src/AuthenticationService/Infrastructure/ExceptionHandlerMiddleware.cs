using Demo.AuthService.Models;
using System.Net;
using System.Text.Json;

namespace Demo.AuthService.Infrastructure
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next,
           ILogger<ExceptionHandlerMiddleware> logger,
           IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (UserNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {
            _logger.LogError(ex, ex.StackTrace);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            var allMessageText = ex.GetFullErrorMessage();

            var details = _env.IsDevelopment() && code == HttpStatusCode.InternalServerError
                ? ex.StackTrace?.ToString() :
                  string.Empty;

            await response.WriteAsync(JsonSerializer.Serialize(new BaseErrorResponse((int)code, allMessageText, details)))
                    .ConfigureAwait(false);
        }
    }
}
