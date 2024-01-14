using ecommerce.API.Utilities.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ecommerce.API.Events.Jwt
{
    public class ReformatUnauthorized
    {
        public static async Task Handle(JwtBearerChallengeContext context)
        {
            context.HandleResponse();

            var json = JsonUtility.Fail("Unauthorized", StatusCodes.Status401Unauthorized);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(JsonUtility.Stringify(json));
        }
    }
}
