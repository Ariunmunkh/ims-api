using Connection.Model;
using HouseHolds.Models;
using System.Threading.Tasks;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseRepository
    {
        #region District

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetDistrictList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetDistrict(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetDistrict(district request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteDistrict(int id);

        #endregion

        #region Relationship

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetRelationshipList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetRelationship(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetRelationship(relationship request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteRelationship(int id);

        #endregion

        #region Relationship

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult GetDropDownItemList(string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult GetDropDownItem(int id, string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetDropDownItem(dropdownitem request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult DeleteDropDownItem(int id, string type);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<MResult> GetSurvey();
    }
}