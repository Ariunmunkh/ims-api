using Connection.Model;
using HouseHolds.Models;

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

    }
}