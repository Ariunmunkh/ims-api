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
        /// <returns></returns>
        MResult GetHouseHoldList();

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
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteHouseHold(int id);

        #endregion

        #region householdmember

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseHoldMemberList();

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