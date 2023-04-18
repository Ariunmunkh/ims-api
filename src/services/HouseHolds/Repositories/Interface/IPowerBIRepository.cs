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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdBusiness();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdBusinessType();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdInvestment();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdInvestmentPrice();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdLivelihoodTraining();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdTechnicalSkillsTraining();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdImprovement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdBasicFinancialTraining();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdHouseholdgroup();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetCoachHouseholdgroup();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdLoan();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdIncomeAndExpenditureRecords();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdlifeSkillsTraining();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult GetHouseholdTraining(int type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult GetHouseholdMediatedactivity(int type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdVisit();

    }
}