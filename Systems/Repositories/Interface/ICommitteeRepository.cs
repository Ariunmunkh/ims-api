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
        MResult GetRepoertExcel();

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

        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <param name="committeeid"></param>
        /// <returns></returns>
        MResult GetRepoertInfoList(int committeeid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetRepoertInfo(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetReportInfo(CommitteeReportInfo request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteReportInfo(int id);

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetcommitteeactivityList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getcommitteeactivity(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setcommitteeactivity(Committeeactivity request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deletecommitteeactivity(int id);
    }
}