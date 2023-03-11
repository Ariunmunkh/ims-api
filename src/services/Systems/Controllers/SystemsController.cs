using System;
using Microsoft.AspNetCore.Mvc;
using Systems.Repositories;
using Systems.Models;
using Infrastructure;
using Connection.Model;

namespace Systems.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SystemsController : ControllerBase
    {
        private readonly ISystemsRepository _SystemsRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemsRepository"></param>
        public SystemsController(ISystemsRepository SystemsRepository)
        {
            _SystemsRepository = SystemsRepository ?? throw new ArgumentNullException(nameof(SystemsRepository));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("password_recovery")]
        public IActionResult PasswordRecovery(string username, string email)
        {
            return Ok(_SystemsRepository.PasswordRecovery(username, email));
        }
    }
}
