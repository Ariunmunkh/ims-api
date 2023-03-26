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

        /// <summary>
        /// Бизнесээ сонгосон нийт гол гишүүний тоо, эзлэх хувь%,  хүйсээр, /нийт, дүүрэг, хороо, коуч -ээр /
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_business")]
        public IActionResult GetHouseholdBusiness()
        {
            return Ok(_PowerBIRepository.GetHouseholdBusiness());
        }

        /// <summary>
        /// Сонгосон бизнесийн төрлүүд: үйлдвэрлэл, үйлчилгээ, худалдаа эзлэх хувиар / нийт, дүүрэг, хороо, коучээр / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_business_type")]
        public IActionResult GetHouseholdBusinessType()
        {
            return Ok(_PowerBIRepository.GetHouseholdBusinessType());
        }

        /// <summary>
        /// Нийт тоног төхөөрөмжийн дэмжлэг авсан өрхийн  тоо  /нийт, дүүрэг, хороо, коуч  / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_investment")]
        public IActionResult GetHouseholdInvestment()
        {
            return Ok(_PowerBIRepository.GetHouseholdInvestment());
        }

        /// <summary>
        /// Нийт хүлээлгэн өгсөн тоног төхөөрөмжийн нийт үнэ болон дундаж  үнэ  / нийт, дүүрэг, хороо, коуч / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_investment_price")]
        public IActionResult GetHouseholdInvestmentPrice()
        {
            return Ok(_PowerBIRepository.GetHouseholdInvestmentPrice());
        }

        /// <summary>
        /// Амьжиргааг дэмжих сургалтанд хамрагдсан гол гишүүдийн  тоо,  хүйсээр / нийт, дүүрэг, хороо, коуч, сургалтын нэр , төрөл, сургалт зохион байгуулсан сараар/ 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_livelihood_training")]
        public IActionResult GetHouseholdLivelihoodTraining()
        {
            return Ok(_PowerBIRepository.GetHouseholdLivelihoodTraining());
        }

        /// <summary>
        /// Техникийн ур чадвар олгох сургалтанд хамрагдсан  хүний тоо, хүйсээр, өрхөөр  / нийт, дүүрэг, хороо, коуч, сургалтын төрөл , сургалт зохион байгуулсан сар / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_technical_skills_training")]
        public IActionResult GetHouseholdTechnicalSkillsTraining()
        {
            return Ok(_PowerBIRepository.GetHouseholdTechnicalSkillsTraining());
        }

        /// <summary>
        /// Аж ахуй эрхлэлтийг дэд салбараар бизнес эрхлэгчдийн тоо, хүйсээр / нийт, дүүрэг, хороо, коуч , дэд салбарын төрлөөр өрхийг оруулах/
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_improvement")]
        public IActionResult GetHouseholdImprovement()
        {
            return Ok(_PowerBIRepository.GetHouseholdImprovement());
        }

        /// <summary>
        /// Санхүүгийн анхан шатны сургалтанд хамрагдсан өрхийн гол гишүүний тоо, эзлэх хувь%,  хүйсээр /нийт, дүүрэг, хороо, коуч , сургалтын нэр, сар /
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_basic_financial_training")]
        public IActionResult GetHouseholdBasicFinancialTraining()
        {
            return Ok(_PowerBIRepository.GetHouseholdBasicFinancialTraining());
        }

        /// <summary>
        /// Дундын хадгаламжийн бүлэгт хамрагдсан хүний тоо, гол гишүүний хүйсээр / нийт, дүүрэг, хороо, коуч /
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_householdgroup")]
        public IActionResult GetHouseholdHouseholdgroup()
        {
            return Ok(_PowerBIRepository.GetHouseholdHouseholdgroup());
        }

        /// <summary>
        /// Коучийн хариуцаж буй бүлгийн тоо /нийт , дүүрэг, хороо / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_coach_householdgroup")]
        public IActionResult GetCoachHouseholdgroup()
        {
            return Ok(_PowerBIRepository.GetCoachHouseholdgroup());
        }

        /// <summary>
        /// Дундын хадгаламжийн  бүлэгт хамрагдсан нийт өрхийн эзлэх хувь  / нийт, дүүрэг, хороо, коуч, гол гишүүний хүйсээр / 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_household_loan")]
        public IActionResult GetHouseholdLoan()
        {
            return Ok(_PowerBIRepository.GetHouseholdLoan());
        }

    }
}
