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
    [Route("api/[controller]")]
    public class CommitteeController : ControllerBase
    {
        private readonly ICommitteeRepository _CommitteeRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommitteeRepository"></param>
        public CommitteeController(ICommitteeRepository CommitteeRepository)
        {
            _CommitteeRepository = CommitteeRepository ?? throw new ArgumentNullException(nameof(CommitteeRepository));
        }

        #region Committee

        /// <summary>
        /// Дунд шатны хорооны сарын тайлан авах
        /// </summary>
        /// <param name="committeeid">Дунд шатны хороо</param>
        /// <returns></returns>
        [HttpGet("get_report_list")]
        public IActionResult GetRepoertList(int committeeid)
        {
            return Ok(_CommitteeRepository.GetRepoertList(committeeid));
        }

        /// <summary>
        /// Дунд шатны хорооны сарын тайлан дэлгэрэнгүй авах
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_report")]
        public IActionResult GetRepoert(int id)
        {
            return Ok(_CommitteeRepository.GetRepoert(id));
        }

        /// <summary>
        /// Дунд шатны хорооны сарын тайлан хадгалах
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_report")]
        public IActionResult SetReport([FromBody] CommitteeReport request)
        {
            return Ok(_CommitteeRepository.SetReport(request));
        }

        /// <summary>
        /// Дунд шатны хорооны бүртгэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_report")]
        public IActionResult DeleteReport(int id)
        {
            return Ok(_CommitteeRepository.DeleteReport(id));
        }

        #endregion

    }
}
