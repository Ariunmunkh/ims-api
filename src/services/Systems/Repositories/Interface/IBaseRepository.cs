using Connection.Model;
using Systems.Models;
using System.Threading.Tasks;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseRepository
    {
        #region DropDown

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
        MResult SetDropDownItem(DropDownItem request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        MResult DeleteDropDownItem(int id, string type);

        #endregion

        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetCommitteeList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetCommittee(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetCommittee(Committee request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteCommittee(int id);

        #endregion

        #region Project

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetProjectList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetProject(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetProject(Project request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteProject(int id);

        #endregion

    }
}