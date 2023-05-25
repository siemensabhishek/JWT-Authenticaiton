using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class DeafultController : ControllerBase
    {
        private IConfiguration _config;

        public DeafultController(IConfiguration config) {
            _config = config;
        }

        [HttpGet("")]
        public string Connection()
        {
            return $"The coonection string is {Environment.NewLine} {_config.GetConnectionString("CustomerDB")}";
        }
    }
}
