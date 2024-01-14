using ecommerce.API.Utilities.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ecommerce.API.Events.Jwt
{
    public class ReformatForbidden
    {
        public static async Task Handle(ForbiddenContext context)
        {
            var json = JsonUtility.Fail("Forbidden", StatusCodes.Status403Forbidden);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync(JsonUtility.Stringify(json));
        }
    }
}
