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

            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    coachid,
    name,
    phone,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetCoach(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    coachid,
    name,
    phone,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
where coachid = @coachid");
            command.AddParam("@coachid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetCoach(coach request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.coachid == 0)
            {
                command.CommandText(@"select coalesce(max(coachid),0)+1 newid from coach");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.coachid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into coach
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

            command.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("select count(1) too from household where coachid = @coachid");
            command.AddParam("@coachid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн бүртгэлд ашигласан тул устгах боломжгүй." };

            command.CommandText("delete from coach where coachid = @coachid");
            return connector.Execute(ref command, false);

        }

        #endregion

        #region HouseholdVisit

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetHouseholdVisitList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    visitid,
    householdid,
    DATE_FORMAT(visitdate, '%Y-%m-%d %H:%i:%s') visitdate,
    memberid,
    coachid,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdvisit
order by updated desc");
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseholdVisit(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
            command.AddParam("@visitid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseholdVisit(householdvisit request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.visitid == 0)
            {
                command.CommandText(@"select coalesce(max(visitid),0)+1 newid from householdvisit");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.visitid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into householdvisit
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

            command.AddParam("@visitid", DbType.Int32, request.visitid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@visitdate", DbType.DateTime, request.visitdate, ParameterDirection.Input);
            command.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            command.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            command.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from householdvisit where visitid = @visitid");
            command.AddParam("@visitid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region meetingattendance

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetmeetingattendanceList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(meetingdate, '%Y-%m-%d %H:%i:%s') meetingdate,
    isjoin,
    quantity,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
order by updated desc");
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getmeetingattendance(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(meetingdate, '%Y-%m-%d %H:%i:%s') meetingdate,
    isjoin,
    quantity,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setmeetingattendance(meetingattendance request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from meetingattendance");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into meetingattendance
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@meetingdate", DbType.DateTime, request.meetingdate, ParameterDirection.Input);
            command.AddParam("@isjoin", DbType.Boolean, request.isjoin, ParameterDirection.Input);
            command.AddParam("@quantity", DbType.Int32, request.quantity, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from meetingattendance where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region loan

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetloanList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(loandate, '%Y-%m-%d %H:%i:%s') loandate,
    amount,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
order by updated desc");
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getloan(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(loandate, '%Y-%m-%d %H:%i:%s') loandate,
    amount,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setloan(loan request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from loan");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into loan
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@loandate", DbType.DateTime, request.loandate, ParameterDirection.Input);
            command.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
            command.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from loan where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region loanrepayment

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetloanrepaymentList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(repaymentdate, '%Y-%m-%d %H:%i:%s') repaymentdate,
    amount,
    balance,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
order by updated desc");
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getloanrepayment(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(repaymentdate, '%Y-%m-%d %H:%i:%s') repaymentdate,
    amount,
    balance,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setloanrepayment(loanrepayment request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from loanrepayment");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into loanrepayment
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@repaymentdate", DbType.DateTime, request.repaymentdate, ParameterDirection.Input);
            command.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
            command.AddParam("@balance", DbType.Decimal, request.balance, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from loanrepayment where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region training

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GettrainingList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate
    name,
    orgname,
    duration,
    isjoin,
    memberid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    training
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Gettraining(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(trainingdate, '%Y-%m-%d %H:%i:%s') trainingdate
    name,
    orgname,
    duration,
    isjoin,
    memberid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    training
where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Settraining(training request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from training");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into training
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@trainingdate", DbType.DateTime, request.trainingdate, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            command.AddParam("@duration", DbType.Int32, request.duration, ParameterDirection.Input);
            command.AddParam("@isjoin", DbType.Boolean, request.isjoin, ParameterDirection.Input);
            command.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from training where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region improvement

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetimprovementList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(plandate, '%Y-%m-%d %H:%i:%s') plandate
    selectedfarm,
    subbranch,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getimprovement(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(plandate, '%Y-%m-%d %H:%i:%s') plandate
    selectedfarm,
    subbranch,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setimprovement(improvement request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from improvement");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into improvement
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@plandate", DbType.DateTime, request.plandate, ParameterDirection.Input);
            command.AddParam("@selectedfarm", DbType.String, request.selectedfarm, ParameterDirection.Input);
            command.AddParam("@subbranch", DbType.String, request.subbranch, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from improvement where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region investment

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetinvestmentList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getinvestment(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setinvestment(investment request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from investment");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into investment
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@investmentdate", DbType.DateTime, request.investmentdate, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@quantity", DbType.Decimal, request.quantity, ParameterDirection.Input);
            command.AddParam("@unitprice", DbType.Decimal, request.unitprice, ParameterDirection.Input);
            command.AddParam("@totalprice", DbType.Decimal, request.totalprice, ParameterDirection.Input);
            command.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from investment where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region othersupport

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetothersupportList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getothersupport(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setothersupport(othersupport request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from othersupport");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into othersupport
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@supportdate", DbType.DateTime, request.supportdate, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@quantity", DbType.Decimal, request.quantity, ParameterDirection.Input);
            command.AddParam("@unitprice", DbType.Decimal, request.unitprice, ParameterDirection.Input);
            command.AddParam("@totalprice", DbType.Decimal, request.totalprice, ParameterDirection.Input);
            command.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from othersupport where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion

        #region mediatedactivity

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetmediatedactivityList()
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    entryid,
    householdid,
    DATE_FORMAT(mediateddate, '%Y-%m-%d %H:%i:%s') mediateddate,
    orgname,
    servicename,
    memberid, 
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    mediatedactivity
order by updated desc");
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Getmediatedactivity(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setmediatedactivity(mediatedactivity request)
        {
            MCommand command = connector.PopCommand();
            MResult result;

            if (request.entryid == 0)
            {
                command.CommandText(@"select coalesce(max(entryid),0)+1 newid from mediatedactivity");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.entryid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into mediatedactivity
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

            command.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@mediateddate", DbType.DateTime, request.mediateddate, ParameterDirection.Input);
            command.AddParam("@orgname", DbType.String, request.orgname, ParameterDirection.Input);
            command.AddParam("@servicename", DbType.String, request.servicename, ParameterDirection.Input);
            command.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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
            MCommand command = connector.PopCommand();
            command.CommandText("delete from mediatedactivity where entryid = @entryid");
            command.AddParam("@entryid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        #endregion

    }
}