using Connection.Model;
using Microsoft.AspNetCore.Mvc;
using Systems.Models;
using Systems.Repositories;

namespace Systems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemsController : ControllerBase
    {
        private readonly ISystemsRepository _SystemsRepository;
        private readonly ILogger<SystemsController> _logger;

        public SystemsController(ILogger<SystemsController> logger, ISystemsRepository SystemsRepository)
        {
            _logger = logger;
            _SystemsRepository = SystemsRepository;
        }

        /// <summary>Нэвтрэх.</summary>
        /// <param name="request">Хэрэглэгчийн мэдээлэл.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] authbody request)
        {
            request.encryptpass = Utility.EncryptPass(request.password);

            MResult result = _SystemsRepository.GetUserInfo(request);
            if (result.rettype == 0)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
    }
}