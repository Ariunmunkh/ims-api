using Connection.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Systems.Models;
using Systems.Repositories;

namespace Systems.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserRepository"></param>
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("get_user_list")]
        public IActionResult GetUserList()
        {
            return Ok(_UserRepository.GetUserList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost("get_user")]
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
