using Microsoft.AspNetCore.Mvc;
using System;
using HouseHolds.Models;
using HouseHolds.Repositories;
using System.Threading.Tasks;

namespace HouseHolds.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/record/powerbi/")]
    public class PowerBIController : ControllerBase
    {
        private readonly IPowerBIRepository _PowerBIRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PowerBIRepository"></param>
        public PowerBIController(IPowerBIRepository PowerBIRepository)
        {
            _PowerBIRepository = PowerBIRepository ?? throw new ArgumentNullException(nameof(PowerBIRepository));
        }

        /// <summary>
        /// Нийт өрхийн тоо/нийт, хороо, дүүрэг, коучээр харах/ 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_count")]
        public IActionResult GetHouseholdCount()
        {
            return Ok(_PowerBIRepository.GetHouseholdCount());
        }

        /// <summary>
        /// 18-55 насны хөдөлмөрийн насны гишүүний тоо/нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_member_18_55_count")]
        public IActionResult GetHouseholdMember1855Count()
        {
            return Ok(_PowerBIRepository.GetHouseholdMember1855Count());
        }

        /// <summary>
        /// Нийт сургуулийн насны хүүхдийн тоо, /нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_member_6_17_count")]
        public IActionResult GetHouseholdMember617Count()
        {
            return Ok(_PowerBIRepository.GetHouseholdMember617Count());
        }

        /// <summary>
        /// Нийт цэцэрлэгийн насны хүүхдийн тоо, /нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_member_5_count")]
        public IActionResult GetHouseholdMember5Count()
        {
            return Ok(_PowerBIRepository.GetHouseholdMember5Count());
        }

        /// <summary>
        /// Өрх толгойлсон байдал /нийт, хороо, дүүрэг/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_single_count")]
        public IActionResult GetHouseholdMemberSingleCount()
        {
            return Ok(_PowerBIRepository.GetHouseholdMemberSingleCount());
        }

        /// <summary>
        /// Нийт өрхийн гишүүдийн дундаж тоо /хороо, дүүрэг, нийт, өрхийн тэргүүний хүйс/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_member_avg")]
        public IActionResult GetHouseholdMemberAvg()
        {
            return Ok(_PowerBIRepository.GetHouseholdMemberAvg());
        }

        /// <summary>
        /// Гол оролцогч гишүүний хүйс /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_participant")]
        public IActionResult GetHouseholdParticipant()
        {
            return Ok(_PowerBIRepository.GetHouseholdParticipant());
        }

        /// <summary>
        /// Гол оролцогч гишүүний хөгжлийн бэрхшээлтэй байдал, хөгжлийн бэрхшээлтэй эсэх /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_participant_disabled")]
        public IActionResult GetHouseholdParticipantDisabled()
        {
            return Ok(_PowerBIRepository.GetHouseholdParticipantDisabled());
        }

        /// <summary>
        /// Өрхийн хэрэгцээний төрөл /нийт, дүүрэг, хороо, коуч, гол гишүүний хүйсээр/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_needs")]
        public IActionResult GetHouseholdNeeds()
        {
            return Ok(_PowerBIRepository.GetHouseholdNeeds());
        }

        /// <summary>
        /// Нийгмийн хамгааллын үйлчилгээнд холбон зуучлагдсан өрхийн тоо /нийт өрх, дүүрэг, хороо, коуч, холбон зуучлагдсан төрөл  / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_services")]
        public IActionResult GetHouseholdServices()
        {
            return Ok(_PowerBIRepository.GetHouseholdServices());
        }

    }
}
