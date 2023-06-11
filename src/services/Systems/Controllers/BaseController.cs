using Microsoft.AspNetCore.Mvc;
using System;
using Systems.Models;
using Systems.Repositories;
using System.Threading.Tasks;

namespace Systems.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/record/base")]
    public class BaseController : ControllerBase
    {
        private readonly IBaseRepository _BaseRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseRepository"></param>
        public BaseController(IBaseRepository BaseRepository)
        {
            _BaseRepository = BaseRepository ?? throw new ArgumentNullException(nameof(BaseRepository));
        }

        #region dropdownitem

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("get_dropdown_item_list")]
        public IActionResult GetDropDownItemList(string type)
        {
            return Ok(_BaseRepository.GetDropDownItemList(type));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("get_dropdown_item")]
        public IActionResult GetDropDownItem(int id, string type)
        {
            return Ok(_BaseRepository.GetDropDownItem(id, type));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_dropdown_item")]
        public IActionResult SetDropDownItem([FromBody] dropdownitem request)
        {
            return Ok(_BaseRepository.SetDropDownItem(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpDelete("delete_dropdown_item")]
        public IActionResult DeleteDropDownItem(int id, string type)
        {
            return Ok(_BaseRepository.DeleteDropDownItem(id, type));
        }

        #endregion

    }
}
