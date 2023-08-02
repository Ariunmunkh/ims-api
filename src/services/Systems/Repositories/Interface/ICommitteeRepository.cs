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
        MResult GetRepoertList(int committeeid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="committeeid"></param>
        /// <param name="reportdate"></param>
        /// <returns></returns>
        MResult GetRepoert(int committeeid, DateTime reportdate);

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetLocalInfoList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetLocalInfo(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetLocalInfo(LocalInfo request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteLocalInfo(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetCommitteeInfoList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetCommitteeInfo(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetCommitteeInfo(CommitteeInfo request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteCommitteeInfo(int id);
    }
}