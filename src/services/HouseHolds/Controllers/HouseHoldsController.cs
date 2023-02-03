﻿using Microsoft.AspNetCore.Mvc;
using System;
using HouseHolds.Models;
using HouseHolds.Repositories;

namespace HouseHolds.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/record/households")]
    public class HouseHoldsController : ControllerBase
    {
        private readonly IHouseHoldsRepository _HouseHoldsRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HouseHoldsRepository"></param>
        public HouseHoldsController(IHouseHoldsRepository HouseHoldsRepository)
        {
            _HouseHoldsRepository = HouseHoldsRepository ?? throw new ArgumentNullException(nameof(HouseHoldsRepository));
        }

        #region household

        /// <summary>
        /// Өрхийн ерөнхий мэдээлэл
        /// </summary>
        /// <param name="coachid"></param>
        /// <param name="status"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpGet("get_household_list")]
        public IActionResult GetHouseHoldList(int coachid, int status, int group)
        {
            return Ok(_HouseHoldsRepository.GetHouseHoldList(coachid,status,group));
        }

        /// <summary>
        /// Өрхийн ерөнхий мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_household")]
        public IActionResult GetHouseHold(int id)
        {
            return Ok(_HouseHoldsRepository.GetHouseHold(id));
        }

        /// <summary>
        /// Өрхийн GPS байршил
        /// </summary>
        /// <param name="districtid"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        [HttpGet("get_household_location")]
        public IActionResult GetHouseHoldLocation(int districtid, int coachid)
        {
            return Ok(_HouseHoldsRepository.GetHouseHoldLocation(districtid,  coachid));
        }

        /// <summary>
        /// Өрхийн ерөнхий мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_household")]
        public IActionResult SetHouseHold([FromBody] household request)
        {
            return Ok(_HouseHoldsRepository.SetHouseHold(request));
        }

        /// <summary>
        /// Өрхийн бүлгийн мэдээлэл өөрчилөх
        /// </summary>
        /// <param name="householdid">Өрхийн дугаар</param>
        /// <param name="householdgroupid">Бүлгийн дугаар</param>
        /// <returns></returns>
        [HttpPost("set_household_group")]
        public IActionResult SetHouseHoldGroup(int householdid, int householdgroupid)
        {
            return Ok(_HouseHoldsRepository.SetHouseHoldGroup(householdid, householdgroupid));
        }

        /// <summary>
        /// Өрхийн ерөнхий мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_household")]
        public IActionResult DeleteHouseHold(int id)
        {
            return Ok(_HouseHoldsRepository.DeleteHouseHold(id));
        }

        #endregion

        #region householdmember

        /// <summary>
        /// Өрхийн гишүүдийн мэдээлэл
        /// </summary>
        /// <param name="householdid"></param>
        /// <returns></returns>
        [HttpGet("get_householdmember_list")]
        public IActionResult GetHouseHoldMemberList(int householdid)
        {
            return Ok(_HouseHoldsRepository.GetHouseHoldMemberList(householdid));
        }

        /// <summary>
        /// Өрхийн гишүүдийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_householdmember")]
        public IActionResult GetHouseHoldMember(int id)
        {
            return Ok(_HouseHoldsRepository.GetHouseHoldMember(id));
        }

        /// <summary>
        /// Өрхийн гишүүдийн мэдээлэл
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("set_householdmember")]
        public IActionResult SetHouseHoldMember([FromBody] householdmember request)
        {
            return Ok(_HouseHoldsRepository.SetHouseHoldMember(request));
        }

        /// <summary>
        /// Өрхийн гишүүдийн мэдээлэл
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_householdmember")]
        public IActionResult DeleteHouseHoldMember(int id)
        {
            return Ok(_HouseHoldsRepository.DeleteHouseHoldMember(id));
        }

        #endregion
    }
}
