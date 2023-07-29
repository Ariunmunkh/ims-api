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
        public IActionResult SetDropDownItem([FromBody] DropDownItem request)
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

        #region Committee

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_Committee_list")]
        public IActionResult GetCommitteeList()
        {
            return Ok(_BaseRepository.GetCommitteeList());
        }

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_Committee")]
        public IActionResult GetCommittee(int id)
        {
            return Ok(_BaseRepository.GetCommittee(id));
        }

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_Committee")]
        public IActionResult SetCommittee([FromBody] Committee request)
        {
            return Ok(_BaseRepository.SetCommittee(request));
        }

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_Committee")]
        public IActionResult DeleteCommittee(int id)
        {
            return Ok(_BaseRepository.DeleteCommittee(id));
        }

        #endregion

        #region Project

        /// <summary>
        /// Хэрэгжүүлж буй төсөл, хөтөлбөр
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_Project_list")]
        public IActionResult GetProjectList()
        {
            return Ok(_BaseRepository.GetProjectList());
        }

        /// <summary>
        /// Хэрэгжүүлж буй төсөл, хөтөлбөр
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_Project")]
        public IActionResult GetProject(int id)
        {
            return Ok(_BaseRepository.GetProject(id));
        }

        /// <summary>
        /// Хэрэгжүүлж буй төсөл, хөтөлбөр
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_Project")]
        public IActionResult SetProject([FromBody] Project request)
        {
            return Ok(_BaseRepository.SetProject(request));
        }

        /// <summary>
        /// Хэрэгжүүлж буй төсөл, хөтөлбөр
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_Project")]
        public IActionResult DeleteProject(int id)
        {
            return Ok(_BaseRepository.DeleteProject(id));
        }

        #endregion

    }
}
