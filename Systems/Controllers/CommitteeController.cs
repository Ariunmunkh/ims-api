using Connection.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Systems.Models;
using Systems.Repositories;

namespace Systems.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class CommitteeController : ControllerBase
    {
        private readonly ICommitteeRepository _CommitteeRepository;
        private readonly ILogger<CommitteeController> _logger;

        public CommitteeController(ILogger<CommitteeController> logger, ICommitteeRepository CommitteeRepository)
        {
            _logger = logger;
            _CommitteeRepository = CommitteeRepository;
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
        /// <param name="committeeid"></param>
        /// <param name="reportdate"></param>
        /// <returns></returns>
        [HttpGet("get_report")]
        public IActionResult GetRepoert(int committeeid, DateTime reportdate)
        {
            return Ok(_CommitteeRepository.GetRepoert(committeeid, reportdate));
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

        #region localinfo

        /// <summary>
        /// Орон нутгийн талаарх мэдээлэл
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_LocalInfo_list")]
        public IActionResult GetLocalInfoList()
        {
            return Ok(_CommitteeRepository.GetLocalInfoList());
        }

        /// <summary>
        /// Орон нутгийн талаарх мэдээлэл
        /// </summary>
        /// <param name="id">Дунд шатны хороо</param>
        /// <returns></returns>
        [HttpGet("get_LocalInfo")]
        public IActionResult GetLocalInfo(int id)
        {
            return Ok(_CommitteeRepository.GetLocalInfo(id));
        }

        /// <summary>
        /// Орон нутгийн талаарх мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_LocalInfo")]
        public IActionResult SetLocalInfo([FromBody] LocalInfo request)
        {
            return Ok(_CommitteeRepository.SetLocalInfo(request));
        }

        /// <summary>
        /// Орон нутгийн талаарх мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_LocalInfo")]
        public IActionResult DeleteLocalInfo(int id)
        {
            return Ok(_CommitteeRepository.DeleteLocalInfo(id));
        }

        #endregion

        #region CommitteeInfo

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_CommitteeInfo_list")]
        public IActionResult GetCommitteeInfoList()
        {
            return Ok(_CommitteeRepository.GetCommitteeInfoList());
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="id">Дунд шатны хорооны дугаар</param>
        /// <returns></returns>
        [HttpGet("get_CommitteeInfo")]
        public IActionResult GetCommitteeInfo(int id)
        {
            return Ok(_CommitteeRepository.GetCommitteeInfo(id));
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_CommitteeInfo")]
        public IActionResult SetCommitteeInfo([FromBody] CommitteeInfo request)
        {
            return Ok(_CommitteeRepository.SetCommitteeInfo(request));
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="id">Дунд шатны хорооны дугаар</param>
        /// <returns></returns>
        [HttpDelete("delete_CommitteeInfo")]
        public IActionResult DeleteCommitteeInfo(int id)
        {
            return Ok(_CommitteeRepository.DeleteCommitteeInfo(id));
        }

        #endregion

        #region committeeactivity

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_committeeactivity_list")]
        public IActionResult GetcommitteeactivityList()
        {
            return Ok(_CommitteeRepository.GetcommitteeactivityList());
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="id">Дунд шатны хороо</param>
        /// <returns></returns>
        [HttpGet("get_committeeactivity")]
        public IActionResult Getcommitteeactivity(int id)
        {
            return Ok(_CommitteeRepository.Getcommitteeactivity(id));
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_committeeactivity")]
        public IActionResult Setcommitteeactivity([FromBody] committeeactivity request)
        {
            return Ok(_CommitteeRepository.Setcommitteeactivity(request));
        }

        /// <summary>
        /// Дунд шатны хорооны талаарх мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_committeeactivity")]
        public IActionResult Deletecommitteeactivity(int id)
        {
            return Ok(_CommitteeRepository.Deletecommitteeactivity(id));
        }

        #endregion

    }
}