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
    [Route("api/record/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _ReportRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportRepository"></param>
        public ReportController(IReportRepository ReportRepository)
        {
            _ReportRepository = ReportRepository ?? throw new ArgumentNullException(nameof(ReportRepository));
        }

        /// <summary>
        /// Газарын зураг ба өрхүүд
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_location")]
        public IActionResult GetHouseholdLocation(int status)
        {
            return Ok(_ReportRepository.GetHouseholdLocation(status));
        }

        /// <summary>
        /// Өрхийн гишүүд
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_member")]
        public IActionResult GetHouseholdMember(int status)
        {
            return Ok(_ReportRepository.GetHouseholdMember(status));
        }

        /// <summary>
        /// Өрхийн айлчлалын мэдээлэл
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_visit")]
        public IActionResult GetHouseholdVisit(int status)
        {
            return Ok(_ReportRepository.GetHouseholdVisit(status));
        }

        /// <summary>
        /// Өрхийн Зээлийн мэдээлэл
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_loan")]
        public IActionResult GetHouseholdLoan(int status)
        {
            return Ok(_ReportRepository.GetHouseholdLoan(status));
        }

        /// <summary>
        /// Өрхийн Сургалт, үйл ажиллагаа
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_training")]
        public IActionResult GetHouseholdTraining(int status)
        {
            return Ok(_ReportRepository.GetHouseholdTraining(status));
        }

        /// <summary>
        /// Өрхийн Амьжиргааг дэмжих
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_improvement")]
        public IActionResult GetHouseholdImprovement(int status)
        {
            return Ok(_ReportRepository.GetHouseholdImprovement(status));
        }

        /// <summary>
        /// Өрхийн Хүлээн авсан хөрөнгө
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_investment")]
        public IActionResult GetHouseholdInvestment(int status)
        {
            return Ok(_ReportRepository.GetHouseholdInvestment(status));
        }

        /// <summary>
        /// Өрхийн Бусад тусламж, дэмжлэг
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_othersupport")]
        public IActionResult GetHouseholdOthersupport(int status)
        {
            return Ok(_ReportRepository.GetHouseholdOthersupport(status));
        }

        /// <summary>
        /// Өрхийн Холбон зуучилсан үйл ажиллагаа
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_mediatedactivity")]
        public IActionResult GetHouseholdMediatedactivity(int status)
        {
            return Ok(_ReportRepository.GetHouseholdMediatedactivity(status));
        }

        /// <summary>
        /// Нийт өрхийн тоо/нийт, хороо, дүүрэг, коучээр харах/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_household_gender_count")]
        public IActionResult GetHouseholdGenderCount(int status)
        {
            return Ok(_ReportRepository.GetHouseholdGenderCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_household_gender_count_district")]
        public IActionResult GetHouseholdGenderCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetHouseholdGenderCountDistrict(status, districtid));
        }

        /// <summary>
        /// Гол оролцогч гишүүний хүйс /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_participant_gender_count")]
        public IActionResult GetParticipantGenderCount(int status)
        {
            return Ok(_ReportRepository.GetParticipantGenderCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_participant_gender_count_district")]
        public IActionResult GetParticipantGenderCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetParticipantGenderCountDistrict(status, districtid));
        }

        /// <summary>
        /// Гол оролцогч гишүүний нас, насны категориор /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_participant_age_count")]
        public IActionResult GetParticipantAgeCount(int status)
        {
            return Ok(_ReportRepository.GetParticipantAgeCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_participant_age_count_district")]
        public IActionResult GetParticipantAgeCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetParticipantAgeCountDistrict(status, districtid));
        }

        /// <summary>
        /// Гол оролцогч гишүүний хөгжлийн бэрхшээлтэй байдал, хөгжлийн бэрхшээлтэй эсэх /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_participant_disabled_count")]
        public IActionResult GetParticipantDisabledCount(int status)
        {
            return Ok(_ReportRepository.GetParticipantDisabledCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_participant_disabled_count_district")]
        public IActionResult GetParticipantDisabledCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetParticipantDisabledCountDistrict(status, districtid));
        }

        /// <summary>
        /// Өрхийн тэргүүний хүйс /нийт, хороо, дүүрэг/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_househead_gender_count")]
        public IActionResult GetHouseheadGenderCount(int status)
        {
            return Ok(_ReportRepository.GetHouseheadGenderCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_househead_gender_count_district")]
        public IActionResult GetHouseheadGenderCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetHouseheadGenderCountDistrict(status, districtid));
        }


        /// <summary>
        /// Өрх толгойлсон байдал /нийт, хороо, дүүрэг/
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("get_housesingle_gender_count")]
        public IActionResult GetHousesingleGenderCount(int status)
        {
            return Ok(_ReportRepository.GetHousesingleGenderCount(status));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_housesingle_gender_count_district")]
        public IActionResult GetHousesingleGenderCountDistrict(int status, int districtid)
        {
            return Ok(_ReportRepository.GetHousesingleGenderCountDistrict(status, districtid));
        }

        /// <summary>
        /// 18-55 насны хөдөлмөрийн насны гишүүний тоо, /нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр, /
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet("get_household_working_age_count")]
        public IActionResult GetHouseholdWorkingAgeCount(int status, int gender)
        {
            return Ok(_ReportRepository.GetHouseholdWorkingAgeCount(status, gender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_household_working_age_count_district")]
        public IActionResult GetHouseholdWorkingAgeCountDistrict(int status, int gender, int districtid)
        {
            return Ok(_ReportRepository.GetHouseholdWorkingAgeCountDistrict(status, gender, districtid));
        }

        /// <summary>
        /// Нийт сургуулийн насны хүүхдийн тоо, /нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр/
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet("get_household_school_age_count")]
        public IActionResult GetHouseholdSchoolAgeCount(int status, int gender)
        {
            return Ok(_ReportRepository.GetHouseholdSchoolAgeCount(status, gender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_household_school_age_count_district")]
        public IActionResult GetHouseholdSchoolAgeCountDistrict(int status, int gender, int districtid)
        {
            return Ok(_ReportRepository.GetHouseholdSchoolAgeCountDistrict(status, gender, districtid));
        }

        /// <summary>
        /// Нийт цэцэрлэгийн насны хүүхдийн тоо, /нийт, хороо, дүүрэг, коуч, өрхийн тэргүүний хүйсээр/
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet("get_household_kindergarten_age_count")]
        public IActionResult GetHouseholdKindergartenAgeCount(int status, int gender)
        {
            return Ok(_ReportRepository.GetHouseholdKindergartenAgeCount(status, gender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_household_kindergarten_age_count_district")]
        public IActionResult GetHouseholdKindergartenAgeCountDistrict(int status, int gender, int districtid)
        {
            return Ok(_ReportRepository.GetHouseholdKindergartenAgeCountDistrict(status, gender, districtid));
        }

        /// <summary>
        /// Нийт өрхийн гишүүдийн дундаж тоо /хороо, дүүрэг, нийт, өрхийн тэргүүний хүйс/
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet("get_housemenber_avg")]
        public IActionResult GetHousemenberAvg(int status, int gender)
        {
            return Ok(_ReportRepository.GetHousemenberAvg(status, gender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_housemenber_avg_district")]
        public IActionResult GetHousemenberAvgDistrict(int status, int gender, int districtid)
        {
            return Ok(_ReportRepository.GetHousemenberAvgDistrict(status, gender, districtid));
        }

        /// <summary>
        /// Нийт хөгжлийн бэрхшээлтэй гишүүний дундаж тоо /нийт, хороо, дүүрэг, коучээр/
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet("get_disabled_avg")]
        public IActionResult GetDisabledAvg(int status, int gender)
        {
            return Ok(_ReportRepository.GetDisabledAvg(status, gender));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        [HttpGet("get_disabled_avg_district")]
        public IActionResult GetDisabledAvgDistrict(int status, int gender, int districtid)
        {
            return Ok(_ReportRepository.GetDisabledAvgDistrict(status, gender, districtid));
        }

    }
}
