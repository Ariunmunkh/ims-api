using Connection.Model;
using HouseHolds.Models;
using System.Threading.Tasks;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPowerBIRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdCount();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdMember1855Count();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdMember617Count();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdMember5Count();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdMemberSingleCount();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdMemberAvg();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdParticipant();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdParticipantDisabled();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdNeeds();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdServices();


    }
}