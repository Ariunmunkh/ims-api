using Connection.Model;
using HouseHolds.Models;
using System.Threading.Tasks;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReportRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetHouseholdGenderCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHouseholdGenderCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetParticipantGenderCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetParticipantGenderCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetParticipantAgeCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetParticipantAgeCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetParticipantDisabledCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetParticipantDisabledCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetHouseheadGenderCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHouseheadGenderCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        MResult GetHousesingleGenderCount(int status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHousesingleGenderCountDistrict(int status, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        MResult GetHouseholdWorkingAgeCount(int status, int gender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHouseholdWorkingAgeCountDistrict(int status, int gender, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        MResult GetHouseholdSchoolAgeCount(int status, int gender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHouseholdSchoolAgeCountDistrict(int status, int gender, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        MResult GetHouseholdKindergartenAgeCount(int status, int gender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHouseholdKindergartenAgeCountDistrict(int status, int gender, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        MResult GetHousemenberAvg(int status, int gender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetHousemenberAvgDistrict(int status, int gender, int districtid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        MResult GetDisabledAvg(int status, int gender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="gender"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        MResult GetDisabledAvgDistrict(int status, int gender, int districtid);

    }
}