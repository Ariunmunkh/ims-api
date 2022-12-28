using Microsoft.AspNetCore.Mvc;
using System;
using HouseHolds.Models;
using HouseHolds.Repositories;

namespace HouseHolds.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/record/coach")]
    public class CoachController : ControllerBase
    {
        private readonly ICoachRepository _CoachRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CoachRepository"></param>
        public CoachController(ICoachRepository CoachRepository)
        {
            _CoachRepository = CoachRepository ?? throw new ArgumentNullException(nameof(CoachRepository));
        }

        #region coach

        /// <summary>
        /// Коуч
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_coach_list")]
        public IActionResult GetCoachList()
        {
            return Ok(_CoachRepository.GetCoachList());
        }

        /// <summary>
        /// Коуч
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_coach")]
        public IActionResult GetCoach(int id)
        {
            return Ok(_CoachRepository.GetCoach(id));
        }

        /// <summary>
        /// Коуч
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_coach")]
        public IActionResult SetCoach([FromBody] coach request)
        {
            return Ok(_CoachRepository.SetCoach(request));
        }

        /// <summary>
        /// Коуч
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_coach")]
        public IActionResult DeleteCoach(int id)
        {
            return Ok(_CoachRepository.DeleteCoach(id));
        }

        #endregion

        #region householdvisit

        /// <summary>
        /// Өрхийн айлчлалын мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_householdvisit_list")]
        public IActionResult GetHouseholdVisitList(int id)
        {
            return Ok(_CoachRepository.GetHouseholdVisitList(id));
        }

        /// <summary>
        /// Өрхийн айлчлалын мэдээлэл
        /// </summary>
        /// <param name="id">visitid</param>
        /// <returns></returns>
        [HttpGet("get_householdvisit")]
        public IActionResult GetHouseholdVisit(int id)
        {
            return Ok(_CoachRepository.GetHouseholdVisit(id));
        }

        /// <summary>
        /// Өрхийн айлчлалын мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_householdvisit")]
        public IActionResult SetHouseholdVisit([FromBody] householdvisit request)
        {
            return Ok(_CoachRepository.SetHouseholdVisit(request));
        }

        /// <summary>
        /// Өрхийн айлчлалын мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_householdvisit")]
        public IActionResult DeleteHouseholdVisit(int id)
        {
            return Ok(_CoachRepository.DeleteHouseholdVisit(id));
        }

        #endregion

        #region meetingattendance

        /// <summary>
        /// Хурлын ирцийн мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_meetingattendance_list")]
        public IActionResult GetmeetingattendanceList(int id)
        {
            return Ok(_CoachRepository.GetmeetingattendanceList(id));
        }

        /// <summary>
        /// Хурлын ирцийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_meetingattendance")]
        public IActionResult Getmeetingattendance(int id)
        {
            return Ok(_CoachRepository.Getmeetingattendance(id));
        }

        /// <summary>
        /// Хурлын ирцийн мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_meetingattendance")]
        public IActionResult Setmeetingattendance([FromBody] meetingattendance request)
        {
            return Ok(_CoachRepository.Setmeetingattendance(request));
        }

        /// <summary>
        /// Хурлын ирцийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_meetingattendance")]
        public IActionResult Deletemeetingattendance(int id)
        {
            return Ok(_CoachRepository.Deletemeetingattendance(id));
        }

        #endregion

        #region loan

        /// <summary>
        /// Зээлийн мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_loan_list")]
        public IActionResult GetloanList(int id)
        {
            return Ok(_CoachRepository.GetloanList(id));
        }

        /// <summary>
        /// Зээлийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_loan")]
        public IActionResult Getloan(int id)
        {
            return Ok(_CoachRepository.Getloan(id));
        }

        /// <summary>
        /// Зээлийн мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_loan")]
        public IActionResult Setloan([FromBody] loan request)
        {
            return Ok(_CoachRepository.Setloan(request));
        }

        /// <summary>
        /// Зээлийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_loan")]
        public IActionResult Deleteloan(int id)
        {
            return Ok(_CoachRepository.Deleteloan(id));
        }

        #endregion

        #region loanrepayment

        /// <summary>
        /// Зээлийн эргэн төлөлтийн мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_loanrepayment_list")]
        public IActionResult GetloanrepaymentList(int id)
        {
            return Ok(_CoachRepository.GetloanrepaymentList(id));
        }

        /// <summary>
        /// Зээлийн эргэн төлөлтийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_loanrepayment")]
        public IActionResult Getloanrepayment(int id)
        {
            return Ok(_CoachRepository.Getloanrepayment(id));
        }

        /// <summary>
        /// Зээлийн эргэн төлөлтийн мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_loanrepayment")]
        public IActionResult Setloanrepayment([FromBody] loanrepayment request)
        {
            return Ok(_CoachRepository.Setloanrepayment(request));
        }

        /// <summary>
        /// Зээлийн эргэн төлөлтийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_loanrepayment")]
        public IActionResult Deleteloanrepayment(int id)
        {
            return Ok(_CoachRepository.Deleteloanrepayment(id));
        }

        #endregion

        #region training

        /// <summary>
        /// Сургалт, үйл ажиллагаа
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_training_list")]
        public IActionResult GettrainingList(int id)
        {
            return Ok(_CoachRepository.GettrainingList(id));
        }

        /// <summary>
        /// Сургалт, үйл ажиллагаа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_training")]
        public IActionResult Gettraining(int id)
        {
            return Ok(_CoachRepository.Gettraining(id));
        }

        /// <summary>
        /// Сургалт, үйл ажиллагаа
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_training")]
        public IActionResult Settraining([FromBody] training request)
        {
            return Ok(_CoachRepository.Settraining(request));
        }

        /// <summary>
        /// Сургалт, үйл ажиллагаа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_training")]
        public IActionResult Deletetraining(int id)
        {
            return Ok(_CoachRepository.Deletetraining(id));
        }

        #endregion

        #region improvement

        /// <summary>
        /// Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_improvement_list")]
        public IActionResult GetimprovementList(int id)
        {
            return Ok(_CoachRepository.GetimprovementList(id));
        }

        /// <summary>
        /// Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_improvement")]
        public IActionResult Getimprovement(int id)
        {
            return Ok(_CoachRepository.Getimprovement(id));
        }

        /// <summary>
        /// Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_improvement")]
        public IActionResult Setimprovement([FromBody] improvement request)
        {
            return Ok(_CoachRepository.Setimprovement(request));
        }

        /// <summary>
        /// Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_improvement")]
        public IActionResult Deleteimprovement(int id)
        {
            return Ok(_CoachRepository.Deleteimprovement(id));
        }

        #endregion

        #region investment

        /// <summary>
        /// Хөрөнгө оруулалтын мэдээлэл
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_investment_list")]
        public IActionResult GetinvestmentList(int id)
        {
            return Ok(_CoachRepository.GetinvestmentList(id));
        }

        /// <summary>
        /// Хөрөнгө оруулалтын мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_investment")]
        public IActionResult Getinvestment(int id)
        {
            return Ok(_CoachRepository.Getinvestment(id));
        }

        /// <summary>
        /// Хөрөнгө оруулалтын мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_investment")]
        public IActionResult Setinvestment([FromBody] investment request)
        {
            return Ok(_CoachRepository.Setinvestment(request));
        }

        /// <summary>
        /// Хөрөнгө оруулалтын мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_investment")]
        public IActionResult Deleteinvestment(int id)
        {
            return Ok(_CoachRepository.Deleteinvestment(id));
        }

        #endregion

        #region othersupport

        /// <summary>
        /// Бусад тусламж, дэмжлэг
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_othersupport_list")]
        public IActionResult GetothersupportList(int id)
        {
            return Ok(_CoachRepository.GetothersupportList(id));
        }

        /// <summary>
        /// Бусад тусламж, дэмжлэг
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_othersupport")]
        public IActionResult Getothersupport(int id)
        {
            return Ok(_CoachRepository.Getothersupport(id));
        }

        /// <summary>
        /// Бусад тусламж, дэмжлэг
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_othersupport")]
        public IActionResult Setothersupport([FromBody] othersupport request)
        {
            return Ok(_CoachRepository.Setothersupport(request));
        }

        /// <summary>
        /// Бусад тусламж, дэмжлэг
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_othersupport")]
        public IActionResult Deleteothersupport(int id)
        {
            return Ok(_CoachRepository.Deleteothersupport(id));
        }

        #endregion

        #region mediatedactivity

        /// <summary>
        /// Холбон зуучилсан үйл ажиллагаа
        /// </summary>
        /// <param name="id">householdid</param>
        /// <returns></returns>
        [HttpGet("get_mediatedactivity_list")]
        public IActionResult GetmediatedactivityList(int id)
        {
            return Ok(_CoachRepository.GetmediatedactivityList(id));
        }

        /// <summary>
        /// Холбон зуучилсан үйл ажиллагаа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_mediatedactivity")]
        public IActionResult Getmediatedactivity(int id)
        {
            return Ok(_CoachRepository.Getmediatedactivity(id));
        }

        /// <summary>
        /// Холбон зуучилсан үйл ажиллагаа
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_mediatedactivity")]
        public IActionResult Setmediatedactivity([FromBody] mediatedactivity request)
        {
            return Ok(_CoachRepository.Setmediatedactivity(request));
        }

        /// <summary>
        /// Холбон зуучилсан үйл ажиллагаа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_mediatedactivity")]
        public IActionResult Deletemediatedactivity(int id)
        {
            return Ok(_CoachRepository.Deletemediatedactivity(id));
        }

        #endregion

    }
}
