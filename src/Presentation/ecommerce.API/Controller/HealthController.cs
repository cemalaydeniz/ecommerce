using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controller
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> HealthCheck()
        {
            return Ok();
        }
    }
}
