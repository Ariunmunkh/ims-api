using Connection.Model;
using Microsoft.AspNetCore.Mvc;
using Systems.Models;
using Systems.Repositories;

namespace Systems.Controllers
{
    [ApiController]
    [Route("api/systems/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository UserRepository)
        {
            _logger = logger;
            _UserRepository = UserRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_user_list")]
        public IActionResult GetUserList()
        {
            return Ok(_UserRepository.GetUserList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("get_user")]
        public IActionResult GetUser(int userid)
        {
            return Ok(_UserRepository.GetUser(userid));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_user")]
        public IActionResult SetUser([FromBody] tbluser request)
        {
            return Ok(_UserRepository.SetUser(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpDelete("delete_user")]
        public IActionResult DeleteUser(int userid)
        {
            return Ok(_UserRepository.DeleteUser(userid));
        }
    }
}