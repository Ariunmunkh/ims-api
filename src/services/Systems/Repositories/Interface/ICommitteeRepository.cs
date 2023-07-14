using Connection.Model;
using Systems.Models;
using System;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommitteeRepository
    {
        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetRepoertList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetRepoert(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetReport(CommitteeReport request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteReport(int id);

        #endregion

    }
}