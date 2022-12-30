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
    coachid,
    name,
    phone,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
order by name asc");
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
updatedby)
values
(@coachid,
@name,
@phone,
@updatedby) 
on duplicate key update 
name=@name,
phone=@phone,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetHouseholdVisitList(int id)
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
left join householdmember on householdmember.memberid = householdvisit.memberid
left join coach on coach.coachid = householdvisit.coachid
where householdvisit.householdid = @householdid
order by householdvisit.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetmeetingattendanceList(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(meetingdate, '%Y-%m-%d %H:%i:%s') meetingdate,
    case
        when isjoin = 0 then 'Үгүй'
        else 'Тийм'
    end isjoin,
    quantity,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
where meetingattendance.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetloanList(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(loandate, '%Y-%m-%d %H:%i:%s') loandate,
    FORMAT(amount,2) amount,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
where loan.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetloanrepaymentList(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(repaymentdate, '%Y-%m-%d %H:%i:%s') repaymentdate,
    FORMAT(amount,2) amount,
    FORMAT(balance,2) balance,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
where loanrepayment.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GettrainingList(int id)
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
left join householdmember on householdmember.memberid = training.memberid
where training.householdid = @householdid
order by training.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetimprovementList(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(plandate, '%Y-%m-%d %H:%i:%s') plandate
    selectedfarm,
    subbranch,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
where improvement.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
    DATE_FORMAT(plandate, '%Y-%m-%d %H:%i:%s') plandate
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
        /// <returns></returns>
        public MResult GetinvestmentList(int id)
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
where investment.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetothersupportList(int id)
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
where othersupport.householdid = @householdid
order by updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
        /// <returns></returns>
        public MResult GetmediatedactivityList(int id)
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
where mediatedactivity.householdid = @householdid
order by mediatedactivity.updated desc");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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