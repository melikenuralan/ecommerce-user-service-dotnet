using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;

        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test-log")]
        public IActionResult TestLog()
        {
            _logger.LogInformation("🟢 RoleController test-log çağrıldı.");
            _logger.LogWarning("⚠️ Bu bir test uyarı mesajıdır.");
            _logger.LogError("❌ Bu bir test hata mesajıdır.");

            return Ok("Loglama başarılı.");
        }
    }
}
