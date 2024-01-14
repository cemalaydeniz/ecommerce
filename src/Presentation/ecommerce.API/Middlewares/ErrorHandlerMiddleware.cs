using ecommerce.API.Utilities.Json;

namespace ecommerce.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError($"Internal Server Error: {e}\nStack Trace: {e.StackTrace}");

                /**
                 * The internal server errors can be logged and saved somewhere else to track
                 * them easily. It can also allow to see statistical data about the APIs.
                 * e.g. which API has the most problems
                 */

                var json = JsonUtility.Fail("Internal Server Error", StatusCodes.Status500InternalServerError);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonUtility.Stringify(json));
            }
        }
    }
}
