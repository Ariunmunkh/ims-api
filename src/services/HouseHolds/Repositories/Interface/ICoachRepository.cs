﻿using Connection.Model;
using HouseHolds.Models;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICoachRepository
    {
        #region Coach

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetCoachList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetCoach(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetCoach(coach request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteCoach(int id);

        #endregion   
        
        #region HouseholdVisit

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetHouseholdVisitList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult GetHouseholdVisit(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult SetHouseholdVisit(householdvisit request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult DeleteHouseholdVisit(int id);

        #endregion

        #region meetingattendance

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetmeetingattendanceList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getmeetingattendance(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setmeetingattendance(meetingattendance request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deletemeetingattendance(int id);

        #endregion

        #region loan

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetloanList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getloan(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setloan(loan request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deleteloan(int id);

        #endregion

        #region loanrepayment

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetloanrepaymentList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getloanrepayment(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setloanrepayment(loanrepayment request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deleteloanrepayment(int id);

        #endregion

        #region training

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GettrainingList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Gettraining(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Settraining(training request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deletetraining(int id);

        #endregion

        #region improvement

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetimprovementList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getimprovement(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setimprovement(improvement request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deleteimprovement(int id);

        #endregion

        #region investment

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetinvestmentList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getinvestment(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setinvestment(investment request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deleteinvestment(int id);

        #endregion

        #region othersupport

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetothersupportList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getothersupport(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setothersupport(othersupport request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deleteothersupport(int id);

        #endregion

        #region mediatedactivity

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetmediatedactivityList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Getmediatedactivity(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MResult Setmediatedactivity(mediatedactivity request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MResult Deletemediatedactivity(int id);

        #endregion

    }
}