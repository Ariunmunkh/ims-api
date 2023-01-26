using Connection.Model;
using HouseHolds.Models;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHouseHoldsRepository
    {
        #region household

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coachid"></param>
        /// <param name="status"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        MResult GetHouseHoldList(int coachid, int status, int group);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetHouseHold(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetHouseHold(household request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="householdid"></param>
        /// <param name="householdgroupid"></param>
        /// <returns></returns>
        MResult SetHouseHoldGroup(int householdid, int householdgroupid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteHouseHold(int id);

        #endregion

        #region householdmember

        /// <summary>
        /// 
        /// </summary>
        /// <param name="householdid"></param>
        /// <returns></returns>
        MResult GetHouseHoldMemberList(int householdid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetHouseHoldMember(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetHouseHoldMember(householdmember request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteHouseHoldMember(int id);

        #endregion
    }
}