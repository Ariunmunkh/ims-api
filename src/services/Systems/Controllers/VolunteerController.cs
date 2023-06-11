using Microsoft.AspNetCore.Mvc;
using System;
using Systems.Models;
using Systems.Repositories;

namespace Systems.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerRepository _VolunteerRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolunteerRepository"></param>
        public VolunteerController(IVolunteerRepository VolunteerRepository)
        {
            _VolunteerRepository = VolunteerRepository ?? throw new ArgumentNullException(nameof(VolunteerRepository));
        }

        #region Volunteer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_Volunteer_list")]
        public IActionResult GetVolunteerList()
        {
            return Ok(_VolunteerRepository.GetVolunteerList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_Volunteer")]
        public IActionResult GetVolunteer(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteer(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_Volunteer")]
        public IActionResult SetVolunteer([FromBody] Volunteer request)
        {
            return Ok(_VolunteerRepository.SetVolunteer(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_Volunteer")]
        public IActionResult DeleteVolunteer(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteer(id));
        }

        #endregion

        #region EmergencyContact

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_EmergencyContact_list")]
        public IActionResult GetEmergencyContactList()
        {
            return Ok(_VolunteerRepository.GetEmergencyContactList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_EmergencyContact")]
        public IActionResult GetEmergencyContact(int id)
        {
            return Ok(_VolunteerRepository.GetEmergencyContact(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_EmergencyContact")]
        public IActionResult SetEmergencyContact([FromBody] EmergencyContact request)
        {
            return Ok(_VolunteerRepository.SetEmergencyContact(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_EmergencyContact")]
        public IActionResult DeleteEmergencyContact(int id)
        {
            return Ok(_VolunteerRepository.DeleteEmergencyContact(id));
        }

        #endregion

        #region VolunteerVoluntaryWork

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_VolunteerVoluntaryWork_list")]
        public IActionResult GetVolunteerVoluntaryWorkList()
        {
            return Ok(_VolunteerRepository.GetVolunteerVoluntaryWorkList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_VolunteerVoluntaryWork")]
        public IActionResult GetVolunteerVoluntaryWork(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteerVoluntaryWork(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_VolunteerVoluntaryWork")]
        public IActionResult SetVolunteerVoluntaryWork([FromBody] VolunteerVoluntaryWork request)
        {
            return Ok(_VolunteerRepository.SetVolunteerVoluntaryWork(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_VolunteerVoluntaryWork")]
        public IActionResult DeleteVolunteerVoluntaryWork(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteerVoluntaryWork(id));
        }

        #endregion

        #region VolunteerTraining

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_VolunteerTraining_list")]
        public IActionResult GetVolunteerTrainingList()
        {
            return Ok(_VolunteerRepository.GetVolunteerTrainingList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_VolunteerTraining")]
        public IActionResult GetVolunteerTraining(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteerTraining(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_VolunteerTraining")]
        public IActionResult SetVolunteerTraining([FromBody] VolunteerTraining request)
        {
            return Ok(_VolunteerRepository.SetVolunteerTraining(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_VolunteerTraining")]
        public IActionResult DeleteVolunteerTraining(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteerTraining(id));
        }

        #endregion

        #region VolunteerSkills

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_VolunteerSkills_list")]
        public IActionResult GetVolunteerSkillsList()
        {
            return Ok(_VolunteerRepository.GetVolunteerSkillsList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_VolunteerSkills")]
        public IActionResult GetVolunteerSkills(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteerSkills(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_VolunteerSkills")]
        public IActionResult SetVolunteerSkills([FromBody] VolunteerSkills request)
        {
            return Ok(_VolunteerRepository.SetVolunteerSkills(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_VolunteerSkills")]
        public IActionResult DeleteVolunteerSkills(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteerSkills(id));
        }

        #endregion

        #region VolunteerMembership

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_VolunteerMembership_list")]
        public IActionResult GetVolunteerMembershipList()
        {
            return Ok(_VolunteerRepository.GetVolunteerMembershipList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_VolunteerMembership")]
        public IActionResult GetVolunteerMembership(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteerMembership(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_VolunteerMembership")]
        public IActionResult SetVolunteerMembership([FromBody] VolunteerMembership request)
        {
            return Ok(_VolunteerRepository.SetVolunteerMembership(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_VolunteerMembership")]
        public IActionResult DeleteVolunteerMembership(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteerMembership(id));
        }

        #endregion

        #region VolunteerAssistance

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_VolunteerAssistance_list")]
        public IActionResult GetVolunteerAssistanceList()
        {
            return Ok(_VolunteerRepository.GetVolunteerAssistanceList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_VolunteerAssistance")]
        public IActionResult GetVolunteerAssistance(int id)
        {
            return Ok(_VolunteerRepository.GetVolunteerAssistance(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_VolunteerAssistance")]
        public IActionResult SetVolunteerAssistance([FromBody] VolunteerAssistance request)
        {
            return Ok(_VolunteerRepository.SetVolunteerAssistance(request));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_VolunteerAssistance")]
        public IActionResult DeleteVolunteerAssistance(int id)
        {
            return Ok(_VolunteerRepository.DeleteVolunteerAssistance(id));
        }

        #endregion

    }
}
