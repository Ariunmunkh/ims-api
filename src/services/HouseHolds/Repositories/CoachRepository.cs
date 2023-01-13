using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using HouseHolds.Models;
using System;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CoachRepository : ICoachRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public CoachRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        #region Coach

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetCoachList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    coach.coachid,
    coach.name,
    coach.phone,
    coach.districtid,
    district.name districtname,
    DATE_FORMAT(coach.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
left join district on district.districtid = coach.districtid
order by coach.name asc");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetCoach(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    coachid,
    name,
    phone,
    districtid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
where coachid = @coachid");
            cmd.AddParam("@coachid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetCoach(coach request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.coachid == 0)
            {
                cmd.CommandText(@"select coalesce(max(coachid),0)+1 newid from coach");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.coachid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into coach
(coachid,
name,
phone,
districtid,
updatedby)
values
(@coachid,
@name,
@phone,
@districtid,
@updatedby) 
on duplicate key update 
name=@name,
phone=@phone,
districtid=@districtid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.coachid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteCoach(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("select count(1) too from household where coachid = @coachid");
            cmd.AddParam("@coachid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн бүртгэлд ашигласан тул устгах боломжгүй." };

            cmd.CommandText("delete from coach where coachid = @coachid");
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region HouseholdVisit

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetHouseholdVisitList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    householdvisit.visitid,
    householdvisit.householdid,
    DATE_FORMAT(householdvisit.visitdate, '%Y-%m-%d %H:%i:%s') visitdate,
    householdvisit.memberid,
    householdmember.name membername,
    householdvisit.coachid,
    coach.name coachname,
    householdvisit.note,
    DATE_FORMAT(householdvisit.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdvisit
left join household on household.householdid = householdvisit.householdid
left join householdmember on householdmember.memberid = householdvisit.memberid
left join coach on coach.coachid = householdvisit.coachid
where (householdvisit.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by householdvisit.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseholdVisit(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    visitid,
    householdid,
    DATE_FORMAT(visitdate, '%Y-%m-%d %H:%i:%s') visitdate,
    memberid,
    coachid,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdvisit
where visitid = @visitid");
            cmd.AddParam("@visitid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseholdVisit(householdvisit request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.visitid == 0)
            {
                cmd.CommandText(@"select coalesce(max(visitid),0)+1 newid from householdvisit");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.visitid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.visitdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.visitdate.AddHours(now.Hour);
                request.visitdate.AddMinutes(now.Minute);
                request.visitdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into householdvisit
(visitid,
householdid,
visitdate,
memberid,
coachid,
note,
updatedby)
values
(@visitid,
@householdid,
@visitdate,
@memberid,
@coachid,
@note,
@updatedby) 
on duplicate key update 
householdid=@householdid,
visitdate=@visitdate,
memberid=@memberid,
coachid=@coachid,
note=@note,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@visitid", DbType.Int32, request.visitid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@visitdate", DbType.DateTime, request.visitdate, ParameterDirection.Input);
            cmd.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.visitid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteHouseholdVisit(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from householdvisit where visitid = @visitid");
            cmd.AddParam("@visitid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        #endregion

        #region meetingattendance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetmeetingattendanceList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    meetingattendance.entryid,
    meetingattendance.householdid,
    DATE_FORMAT(meetingattendance.meetingdate, '%Y-%m-%d %H:%i:%s') meetingdate,
    case
        when meetingattendance.isjoin = 0 then 'Үгүй'
        else 'Тийм'
    end isjoin,
    meetingattendance.quantity,
    DATE_FORMAT(meetingattendance.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
left join household on household.householdid = meetingattendance.householdid
where (meetingattendance.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by meetingattendance.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getmeetingattendance(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(meetingdate, '%Y-%m-%d %H:%i:%s') meetingdate,
    isjoin,
    quantity,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setmeetingattendance(meetingattendance request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from meetingattendance");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.meetingdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.meetingdate.AddHours(now.Hour);
                request.meetingdate.AddMinutes(now.Minute);
                request.meetingdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into meetingattendance
(entryid,
householdid,
meetingdate,
isjoin,
quantity,
updatedby)
values
(@entryid,
@householdid,
@meetingdate,
@isjoin,
@quantity,
@updatedby) 
on duplicate key update 
householdid=@householdid,
meetingdate=@meetingdate,
isjoin=@isjoin,
quantity=@quantity,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@meetingdate", DbType.DateTime, request.meetingdate, ParameterDirection.Input);
            cmd.AddParam("@isjoin", DbType.Boolean, request.isjoin, ParameterDirection.Input);
            cmd.AddParam("@quantity", DbType.Int32, request.quantity, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deletemeetingattendance(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from meetingattendance where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region loan

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetloanList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    loan.entryid,
    loan.householdid,
    DATE_FORMAT(loan.loandate, '%Y-%m-%d %H:%i:%s') loandate,
    FORMAT(loan.amount,2) amount,
    loan.note,
    DATE_FORMAT(loan.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
left join household on household.householdid = loan.householdid
where (loan.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by loan.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getloan(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(loandate, '%Y-%m-%d %H:%i:%s') loandate,
    amount,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setloan(loan request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from loan");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.loandate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.loandate.AddHours(now.Hour);
                request.loandate.AddMinutes(now.Minute);
                request.loandate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into loan
(entryid,
householdid,
loandate,
amount,
note,
updatedby)
values
(@entryid,
@householdid,
@loandate,
@amount,
@note,
@updatedby) 
on duplicate key update 
householdid=@householdid,
loandate=@loandate,
amount=@amount,
note=@note,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@loandate", DbType.DateTime, request.loandate, ParameterDirection.Input);
            cmd.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deleteloan(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from loan where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region loanrepayment

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetloanrepaymentList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    loanrepayment.entryid,
    loanrepayment.householdid,
    DATE_FORMAT(loanrepayment.repaymentdate, '%Y-%m-%d %H:%i:%s') repaymentdate,
    FORMAT(loanrepayment.amount,2) amount,
    FORMAT(loanrepayment.balance,2) balance,
    DATE_FORMAT(loanrepayment.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
left join household on household.householdid = loanrepayment.householdid
where (loanrepayment.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by loanrepayment.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getloanrepayment(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(repaymentdate, '%Y-%m-%d %H:%i:%s') repaymentdate,
    amount,
    balance,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setloanrepayment(loanrepayment request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from loanrepayment");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.repaymentdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.repaymentdate.AddHours(now.Hour);
                request.repaymentdate.AddMinutes(now.Minute);
                request.repaymentdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into loanrepayment
(entryid,
householdid,
repaymentdate,
amount,
balance,
updatedby)
values
(@entryid,
@householdid,
@repaymentdate,
@amount,
@balance,
@updatedby) 
on duplicate key update 
householdid=@householdid,
repaymentdate=@repaymentdate,
amount=@amount,
balance=@balance,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@repaymentdate", DbType.DateTime, request.repaymentdate, ParameterDirection.Input);
            cmd.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
            cmd.AddParam("@balance", DbType.Decimal, request.balance, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deleteloanrepayment(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from loanrepayment where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region training

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GettrainingList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    training.entryid,
    training.householdid,
    DATE_FORMAT(training.trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate,
    training.name,
    training.orgname,
    training.duration,
    training.isjoin,
    training.memberid,
    householdmember.name membername,
    DATE_FORMAT(training.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    training
left join household on household.householdid = training.householdid
left join householdmember on householdmember.memberid = training.memberid
where (training.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by training.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Gettraining(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate,
    name,
    orgname,
    duration,
    isjoin,
    memberid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    training
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Settraining(training request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from training");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.trainingdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.trainingdate.AddHours(now.Hour);
                request.trainingdate.AddMinutes(now.Minute);
                request.trainingdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into training
(entryid,
householdid,
trainingdate,
name,
orgname,
duration,
isjoin,
memberid,
updatedby)
values
(@entryid,
@householdid,
@trainingdate,
@name,
@orgname,
@duration,
@isjoin,
@memberid,
@updatedby) 
on duplicate key update 
householdid=@householdid,
trainingdate=@trainingdate,
name=@name,
orgname=@orgname,
duration=@duration,
isjoin=@isjoin,
memberid=@memberid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@trainingdate", DbType.DateTime, request.trainingdate, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            cmd.AddParam("@duration", DbType.Int32, request.duration, ParameterDirection.Input);
            cmd.AddParam("@isjoin", DbType.Boolean, request.isjoin, ParameterDirection.Input);
            cmd.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deletetraining(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from training where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region improvement

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetimprovementList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    improvement.entryid,
    improvement.householdid,
    DATE_FORMAT(improvement.plandate, '%Y-%m-%d %H:%i:%s') plandate,
    improvement.selectedfarm,
    improvement.subbranch,
    DATE_FORMAT(improvement.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
left join household on household.householdid = improvement.householdid
where (improvement.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by improvement.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getimprovement(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(plandate, '%Y-%m-%d %H:%i:%s') plandate,
    selectedfarm,
    subbranch,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setimprovement(improvement request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from improvement");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.plandate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.plandate.AddHours(now.Hour);
                request.plandate.AddMinutes(now.Minute);
                request.plandate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into improvement
(entryid,
householdid,
plandate,
selectedfarm,
subbranch,
updatedby)
values
(@entryid,
@householdid,
@plandate,
@selectedfarm,
@subbranch,
@updatedby) 
on duplicate key update 
householdid=@householdid,
plandate=@plandate,
selectedfarm=@selectedfarm,
subbranch=@subbranch,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@plandate", DbType.DateTime, request.plandate, ParameterDirection.Input);
            cmd.AddParam("@selectedfarm", DbType.String, request.selectedfarm, ParameterDirection.Input);
            cmd.AddParam("@subbranch", DbType.String, request.subbranch, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deleteimprovement(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from improvement where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region investment

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetinvestmentList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    investment.entryid,
    investment.householdid,
    DATE_FORMAT(investment.investmentdate, '%Y-%m-%d %H:%i:%s') investmentdate,
    investment.name,
    investment.quantity,
    investment.unitprice,
    investment.totalprice,
    investment.note,
    DATE_FORMAT(investment.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    investment
left join household on household.householdid = investment.householdid
where (investment.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by investment.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getinvestment(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(investmentdate, '%Y-%m-%d %H:%i:%s') investmentdate,
    name,
    quantity,
    unitprice,
    totalprice,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    investment
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setinvestment(investment request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from investment");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.investmentdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.investmentdate.AddHours(now.Hour);
                request.investmentdate.AddMinutes(now.Minute);
                request.investmentdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into investment
(entryid,
householdid,
investmentdate,
name,
quantity,
unitprice,
totalprice,
note,
updatedby)
values
(@entryid,
@householdid,
@investmentdate,
@name,
@quantity,
@unitprice,
@totalprice,
@note,
@updatedby) 
on duplicate key update 
householdid=@householdid,
investmentdate=@investmentdate,
name=@name,
quantity=@quantity,
unitprice=@unitprice,
totalprice=@totalprice,
note=@note,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@investmentdate", DbType.DateTime, request.investmentdate, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@quantity", DbType.Decimal, request.quantity, ParameterDirection.Input);
            cmd.AddParam("@unitprice", DbType.Decimal, request.unitprice, ParameterDirection.Input);
            cmd.AddParam("@totalprice", DbType.Decimal, request.totalprice, ParameterDirection.Input);
            cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deleteinvestment(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from investment where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region othersupport

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetothersupportList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    othersupport.entryid,
    othersupport.householdid,
    DATE_FORMAT(othersupport.supportdate, '%Y-%m-%d %H:%i:%s') supportdate,
    othersupport.name,
    othersupport.quantity,
    othersupport.unitprice,
    othersupport.totalprice,
    othersupport.orgname,
    DATE_FORMAT(othersupport.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    othersupport
left join household on household.householdid = othersupport.householdid
where (othersupport.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by othersupport.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getothersupport(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(supportdate, '%Y-%m-%d %H:%i:%s') supportdate,
    name,
    quantity,
    unitprice,
    totalprice,
    orgname,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    othersupport
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setothersupport(othersupport request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from othersupport");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.supportdate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.supportdate.AddHours(now.Hour);
                request.supportdate.AddMinutes(now.Minute);
                request.supportdate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into othersupport
(entryid,
householdid,
supportdate,
name,
quantity,
unitprice,
totalprice,
orgname,
updatedby)
values
(@entryid,
@householdid,
@supportdate,
@name,
@quantity,
@unitprice,
@totalprice,
@orgname,
@updatedby) 
on duplicate key update 
householdid=@householdid,
supportdate=@supportdate,
name=@name,
quantity=@quantity,
unitprice=@unitprice,
totalprice=@totalprice,
orgname=@orgname,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@supportdate", DbType.DateTime, request.supportdate, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@quantity", DbType.Decimal, request.quantity, ParameterDirection.Input);
            cmd.AddParam("@unitprice", DbType.Decimal, request.unitprice, ParameterDirection.Input);
            cmd.AddParam("@totalprice", DbType.Decimal, request.totalprice, ParameterDirection.Input);
            cmd.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deleteothersupport(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from othersupport where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region mediatedactivity

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetmediatedactivityList(int id, int coachid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    mediatedactivity.entryid,
    mediatedactivity.householdid,
    DATE_FORMAT(mediatedactivity.mediateddate, '%Y-%m-%d %H:%i:%s') mediateddate,
    mediatedactivity.orgname,
    mediatedactivity.servicename,
    mediatedactivity.memberid, 
    householdmember.name membername, 
    DATE_FORMAT(mediatedactivity.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    mediatedactivity
left join householdmember on householdmember.memberid = mediatedactivity.memberid
inner join household on household.householdid = mediatedactivity.householdid
where (mediatedactivity.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by mediatedactivity.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getmediatedactivity(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(mediateddate, '%Y-%m-%d %H:%i:%s') mediateddate,
    orgname,
    servicename,
    memberid, 
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    mediatedactivity
where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setmediatedactivity(mediatedactivity request)
        {
            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                cmd.CommandText(@"select coalesce(max(entryid),0)+1 newid from mediatedactivity");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            if (request.mediateddate.ToString("HH:mm:ss").Equals("00:00:00"))
            {
                DateTime now = DateTime.Now;
                request.mediateddate.AddHours(now.Hour);
                request.mediateddate.AddMinutes(now.Minute);
                request.mediateddate.AddSeconds(now.Second);
            }

            cmd.CommandText(@"insert into mediatedactivity
(entryid,
householdid,
mediateddate,
orgname,
servicename,
memberid, 
updatedby)
values
(@entryid,
@householdid,
@mediateddate,
@orgname,
@servicename,
@memberid, 
@updatedby) 
on duplicate key update 
householdid=@householdid,
mediateddate=@mediateddate,
orgname=@orgname,
servicename=@servicename,
memberid=@memberid, 
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@mediateddate", DbType.DateTime, request.mediateddate, ParameterDirection.Input);
            cmd.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            cmd.AddParam("@servicename", DbType.String, request.servicename, ParameterDirection.Input);
            cmd.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.entryid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deletemediatedactivity(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from mediatedactivity where entryid = @entryid");
            cmd.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        #endregion

    }
}