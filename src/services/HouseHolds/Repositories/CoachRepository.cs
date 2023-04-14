using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using HouseHolds.Models;
using System;
using System.Collections.Generic;

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

        #region Project

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetProjectList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    project.id,
    project.name,
    project.leadername,
    project.leaderphone,
    project.location,
    project.implementation,
    DATE_FORMAT(project.updated, '%Y-%m-%d %H:%i:%s') updated
from
    project
order by project.updated desc");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetProject(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select 
    project.id,
    project.name,
    project.leadername,
    project.leaderphone,
    project.location,
    project.implementation,
    date_format(project.updated, '%y-%m-%d %h:%i:%s') updated
from
    project
where project.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetProject(project request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from project");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into project
(id,
name,
leadername,
leaderphone,
location,
implementation,
updatedby)
values
(@id,
@name,
@leadername,
@leaderphone,
@location,
@implementation,
@updatedby) 
on duplicate key update 
name=@name,
leadername=@leadername,
leaderphone=@leaderphone,
location=@location,
implementation=@implementation,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@leadername", DbType.String, request.leadername, ParameterDirection.Input);
            cmd.AddParam("@leaderphone", DbType.String, request.leaderphone, ParameterDirection.Input);
            cmd.AddParam("@location", DbType.String, request.location, ParameterDirection.Input);
            cmd.AddParam("@implementation", DbType.String, request.implementation, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.id };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteProject(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from project where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        #endregion

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
    coach.projectid,
    project.name projectname,
    coach.districtid,
    district.name districtname,
    coach.section,
    household.householdcount,
    DATE_FORMAT(coach.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
left join project on project.id = coach.projectid
left join district on district.districtid = coach.districtid
left join (select household.coachid, count(1) householdcount from household group by household.coachid) household on household.coachid = coach.coachid
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
    coach.projectid,
    districtid,
    section section1,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    coach
where coachid = @coachid");
            cmd.AddParam("@coachid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0)
            {
                data.Columns.Add("section", typeof(object));
                if (!string.IsNullOrEmpty(data.Rows[0]["section1"].ToString()))
                    data.Rows[0]["section"] = data.Rows[0]["section1"].ToString().Split(',');
                else
                    data.Rows[0]["section"] = new List<string> { };
                return new MResult { retdata = data };
            }

            return new MResult { };
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
projectid,
districtid,
section,
updatedby)
values
(@coachid,
@name,
@phone,
@projectid,
@districtid,
@section,
@updatedby) 
on duplicate key update 
name=@name,
phone=@phone,
projectid=@projectid,
districtid=@districtid,
section=@section,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            cmd.AddParam("@projectid", DbType.Int32, request.projectid, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@section", DbType.String, request.section != null ? string.Join(",", request.section) : string.Empty, ParameterDirection.Input);
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
    DATE_FORMAT(householdvisit.visitdate,
            '%Y-%m-%d %H:%i:%s') visitdate,
    householdvisit.memberid,
    householdmember.name membername,
    CASE
        WHEN householdvisit.incomeexpenditurerecord = FALSE THEN 'Үгүй'
        WHEN householdvisit.incomeexpenditurerecord = TRUE THEN 'Тийм'
        ELSE 'Хоосон'
    END incomeexpenditurerecord,
    CASE
        WHEN householdvisit.developmentplan = FALSE THEN 'Үгүй'
        WHEN householdvisit.developmentplan = TRUE THEN 'Тийм'
        ELSE 'Хоосон'
    END developmentplan,
    district.name districtname,
    household.section,
    householdvisit.coachid,
    coach.name coachname,
    householdvisit.decisionandaction,
    householdvisit.basicneedsnote,
    householdvisit.note,
    householdvisit_needs.name basicneedsname,
    DATE_FORMAT(householdvisit.updated,
            '%Y-%m-%d %H:%i:%s') updated
FROM
    householdvisit
        LEFT JOIN
    (SELECT 
        householdvisit_needs.visitid,
            GROUP_CONCAT(basicneeds.name
                SEPARATOR ', ') name
    FROM
        householdvisit_needs
    LEFT JOIN basicneeds ON basicneeds.id = householdvisit_needs.basicneedsid
    GROUP BY householdvisit_needs.visitid) householdvisit_needs ON householdvisit_needs.visitid = householdvisit.visitid
        LEFT JOIN
    household ON household.householdid = householdvisit.householdid
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    householdmember ON householdmember.memberid = householdvisit.memberid
        LEFT JOIN
    coach ON coach.coachid = householdvisit.coachid
WHERE
    (householdvisit.householdid = @householdid
        OR 0 = @householdid)
        AND (household.coachid = @coachid
        OR 0 = @coachid)
ORDER BY householdvisit.visitdate DESC");
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
    incomeexpenditurerecord,
    developmentplan,
    decisionandaction,
    basicneedsnote,
    coachid,
    note,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdvisit
where visitid = @visitid");
            cmd.AddParam("@visitid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            DataTable retdata = result.retdata as DataTable;

            cmd.CommandText("select * from householdvisit_needs where visitid = @visitid");
            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable needsdata && needsdata.Rows.Count > 0)
            {
                List<object> needslist = new List<object>();
                foreach (DataRow dr in needsdata.Rows)
                {
                    needslist.Add(dr["basicneedsid"]);
                }
                if (retdata.Rows.Count > 0)
                {
                    retdata.Columns.Add("basicneedsid", typeof(object));
                    retdata.Rows[0]["basicneedsid"] = needslist;
                }
            }

            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseholdVisit(householdvisit request)
        {
            try
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

                request.visitdate = Convert.ToDateTime(DateTime.Now.Year + request.visitdate.ToString(".MM.dd HH:mm:ss"));
                if (!request.incomeexpenditurerecord.HasValue)
                {
                    request.incomeexpenditurerecord = false;
                }

                connector.BeginTransaction();
                cmd.CommandText(@"insert into householdvisit
(visitid,
householdid,
visitdate,
memberid,
coachid,
incomeexpenditurerecord,
developmentplan,
decisionandaction,
basicneedsnote,
note,
updatedby)
values
(@visitid,
@householdid,
@visitdate,
@memberid,
@coachid,
@incomeexpenditurerecord,
@developmentplan,
@decisionandaction,
@basicneedsnote,
@note,
@updatedby) 
on duplicate key update 
householdid=@householdid,
visitdate=@visitdate,
memberid=@memberid,
coachid=@coachid,
incomeexpenditurerecord=@incomeexpenditurerecord,
developmentplan=@developmentplan,
decisionandaction=@decisionandaction,
basicneedsnote=@basicneedsnote,
note=@note,
updated=current_timestamp,
updatedby=@updatedby");

                cmd.AddParam("@visitid", DbType.Int32, request.visitid, ParameterDirection.Input);
                cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
                cmd.AddParam("@visitdate", DbType.DateTime, request.visitdate, ParameterDirection.Input);
                cmd.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
                cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
                cmd.AddParam("@incomeexpenditurerecord", DbType.Boolean, request.incomeexpenditurerecord, ParameterDirection.Input);
                cmd.AddParam("@developmentplan", DbType.Boolean, request.developmentplan, ParameterDirection.Input);
                cmd.AddParam("@decisionandaction", DbType.String, request.decisionandaction, ParameterDirection.Input);
                cmd.AddParam("@basicneedsnote", DbType.String, request.basicneedsnote, ParameterDirection.Input);
                cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                {
                    connector.RollbackTransaction();
                    return result;
                }

                cmd.CommandText("delete from householdvisit_needs where visitid = @visitid");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                {
                    connector.RollbackTransaction();
                    return result;
                }

                if (request.basicneedsid != null && request.basicneedsid.Length > 0)
                {
                    cmd.CommandText("insert into householdvisit_needs (visitid, basicneedsid,updatedby) values(@visitid, @basicneedsid,@updatedby)");
                    cmd.AddParam("@basicneedsid", DbType.Int32, ParameterDirection.Input);

                    foreach (int basicneedsid in request.basicneedsid)
                    {
                        cmd.SetParamValue("@basicneedsid", basicneedsid);
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                        {
                            connector.RollbackTransaction();
                            return result;
                        }
                    }
                }
                connector.CommitTransaction();
                return new MResult { retdata = request.visitid };

            }
            catch (Exception ex)
            {
                if (connector != null && connector.Transacted)
                    connector.RollbackTransaction();

                return new MResult { rettype = 1, retmsg = ex.Message, retdata = ex };
            }
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
    meetingattendance.amount,
    FORMAT(meetingattendance.amount,2) famount,
    householdmember.name,
    householdmember.regno,
    district.name districtname,
    household.section,
    householdgroup.name householdgroupname,
    DATE_FORMAT(meetingattendance.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    meetingattendance
left join household on household.householdid = meetingattendance.householdid
LEFT JOIN householdmember ON householdmember.memberid = household.memberid
LEFT JOIN householdgroup ON householdgroup.id = household.householdgroupid
left join district on district.districtid = household.districtid
where (meetingattendance.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by meetingattendance.meetingdate desc");
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
    amount,
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

            request.meetingdate = Convert.ToDateTime(DateTime.Now.Year + request.meetingdate.ToString(".MM.dd HH:mm:ss"));
            cmd.CommandText(@"insert into meetingattendance
(entryid,
householdid,
meetingdate,
isjoin,
quantity,
amount,
updatedby)
values
(@entryid,
@householdid,
@meetingdate,
@isjoin,
@quantity,
@amount,
@updatedby) 
on duplicate key update 
householdid=@householdid,
meetingdate=@meetingdate,
isjoin=@isjoin,
quantity=@quantity,
amount=@amount,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@meetingdate", DbType.DateTime, request.meetingdate, ParameterDirection.Input);
            cmd.AddParam("@isjoin", DbType.Boolean, request.isjoin, ParameterDirection.Input);
            cmd.AddParam("@quantity", DbType.Int32, request.quantity, ParameterDirection.Input);
            cmd.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
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
    loan.loanpurposenote,
    loan.loanpurposeid,
    loanpurpose.name loanpurpose,
    household.name householdname,
    district.name districtname,
    household.section,
    DATE_FORMAT(loan.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loan
left join household on household.householdid = loan.householdid
left join district on district.districtid = household.districtid
left join loanpurpose on loanpurpose.id = loan.loanpurposeid
where (loan.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by loan.loandate desc");
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
    loanpurposeid,
    loanpurposenote,
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
            request.loandate = Convert.ToDateTime(DateTime.Now.Year + request.loandate.ToString(".MM.dd HH:mm:ss"));
            cmd.CommandText(@"insert into loan
(entryid,
householdid,
loandate,
amount,
loanpurposeid,
loanpurposenote,
updatedby)
values
(@entryid,
@householdid,
@loandate,
@amount,
@loanpurposeid,
@loanpurposenote,
@updatedby) 
on duplicate key update 
householdid=@householdid,
loandate=@loandate,
amount=@amount,
loanpurposeid=@loanpurposeid,
loanpurposenote=@loanpurposenote,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@loandate", DbType.DateTime, request.loandate, ParameterDirection.Input);
            cmd.AddParam("@amount", DbType.Decimal, request.amount, ParameterDirection.Input);
            cmd.AddParam("@loanpurposeid", DbType.Int32, request.loanpurposeid, ParameterDirection.Input);
            cmd.AddParam("@loanpurposenote", DbType.String, request.loanpurposenote, ParameterDirection.Input);
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
    household.name householdname,
    district.name districtname,
    household.section,
    DATE_FORMAT(loanrepayment.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    loanrepayment
left join household on household.householdid = loanrepayment.householdid
left join district on district.districtid = household.districtid
where (loanrepayment.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by loanrepayment.repaymentdate desc");
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

            request.repaymentdate = Convert.ToDateTime(DateTime.Now.Year + request.repaymentdate.ToString(".MM.dd HH:mm:ss"));
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
    training.trainingcategoryid,
    trainingcategory.name trainingcategoryname,
    training.trainingtypeid,
    trainingtype.name trainingtype,
    training.trainingandactivityid,
    trainingandactivity.name trainingandactivity,
    training.formoftrainingid,
    formoftraining.name formoftrainingname,
    training.organizationid,
    organization.name organization,
    training.duration,
    case
        when training.isjoin = 0 then 'Үгүй'
        when training.isjoin = 1 then 'Тийм'
        else 'Хоосон'
    end isjoin,
    training.memberid,
    householdmember.name membername,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        when householdmember.gender = 1 then 'Эмэгтэй'
        else 'Хоосон'
    end gender,
    case
        when householdmember.isparticipant = 0 then 'Үгүй'
        else 'Тийм'
    end isparticipant,
    district.name districtname,
    household.section,
    DATE_FORMAT(training.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    training
left join household on household.householdid = training.householdid
left join district on district.districtid = household.districtid
left join householdmember on householdmember.memberid = training.memberid
left join trainingcategory on trainingcategory.id = training.trainingcategoryid
left join trainingtype on trainingtype.id = training.trainingtypeid
left join trainingandactivity on trainingandactivity.id = training.trainingandactivityid
left join formoftraining on formoftraining.id = training.formoftrainingid
left join organization on organization.id = training.organizationid
where (training.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by training.trainingdate desc");
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
    trainingcategoryid,
    trainingtypeid,
    trainingandactivityid,
    formoftrainingid,
    organizationid,
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
            request.trainingdate = Convert.ToDateTime(DateTime.Now.Year + request.trainingdate.ToString(".MM.dd HH:mm:ss"));

            cmd.CommandText(@"insert into training
(entryid,
householdid,
trainingdate,
trainingcategoryid,
trainingtypeid,
trainingandactivityid,
formoftrainingid,
organizationid,
duration,
isjoin,
memberid,
updatedby)
values
(@entryid,
@householdid,
@trainingdate,
@trainingcategoryid,
@trainingtypeid,
@trainingandactivityid,
@formoftrainingid,
@organizationid,
@duration,
@isjoin,
@memberid,
@updatedby) 
on duplicate key update 
householdid=@householdid,
trainingdate=@trainingdate,
trainingcategoryid=@trainingcategoryid,
trainingtypeid=@trainingtypeid,
trainingandactivityid=@trainingandactivityid,
formoftrainingid=@formoftrainingid,
organizationid=@organizationid,
duration=@duration,
isjoin=@isjoin,
memberid=@memberid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@trainingdate", DbType.DateTime, request.trainingdate, ParameterDirection.Input);
            cmd.AddParam("@trainingcategoryid", DbType.Int32, request.trainingcategoryid, ParameterDirection.Input);
            cmd.AddParam("@trainingtypeid", DbType.Int32, request.trainingtypeid, ParameterDirection.Input);
            cmd.AddParam("@trainingandactivityid", DbType.Int32, request.trainingandactivityid, ParameterDirection.Input);
            cmd.AddParam("@formoftrainingid", DbType.Int32, request.formoftrainingid, ParameterDirection.Input);
            cmd.AddParam("@organizationid", DbType.Int32, request.organizationid, ParameterDirection.Input);
            cmd.AddParam("@duration", DbType.Decimal, request.duration, ParameterDirection.Input);
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
    improvement.businessid,
    business.name businessname,
    improvement.subbranchid,
    subbranch.name subbranch,
    household.name householdname,
    district.name districtname,
    household.section,
    DATE_FORMAT(improvement.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    improvement
left join household on household.householdid = improvement.householdid
left join district on district.districtid = household.districtid
left join business on business.id = improvement.businessid
left join subbranch on subbranch.id = improvement.subbranchid
where (improvement.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by improvement.plandate desc");
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
    businessid,
    subbranchid,
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
            request.plandate = Convert.ToDateTime(DateTime.Now.Year + request.plandate.ToString(".MM.dd HH:mm:ss"));

            cmd.CommandText(@"insert into improvement
(entryid,
householdid,
plandate,
businessid,
subbranchid,
updatedby)
values
(@entryid,
@householdid,
@plandate,
@businessid,
@subbranchid,
@updatedby) 
on duplicate key update 
householdid=@householdid,
plandate=@plandate,
businessid=@businessid,
subbranchid=@subbranchid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@plandate", DbType.DateTime, request.plandate, ParameterDirection.Input);
            cmd.AddParam("@businessid", DbType.String, request.businessid, ParameterDirection.Input);
            cmd.AddParam("@subbranchid", DbType.Int32, request.subbranchid, ParameterDirection.Input);
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
    investment.assetreceivedtypeid,
    assetreceivedtype.name assetreceivedtype,
    investment.assetreceivedid,
    assetreceived.name assetreceived,
    investment.quantity,
    investment.unitprice,
    FORMAT(investment.unitprice,2) funitprice,
    investment.totalprice,
    FORMAT(investment.totalprice,2) ftotalprice,
    investment.note,
    household.name householdname,
    district.name districtname,
    household.section,
    DATE_FORMAT(investment.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    investment
left join household on household.householdid = investment.householdid
left join district on district.districtid = household.districtid
left join assetreceivedtype on assetreceivedtype.id = investment.assetreceivedtypeid
left join assetreceived on assetreceived.id = investment.assetreceivedid
where (investment.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by investment.investmentdate desc");
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
    assetreceivedtypeid,
    assetreceivedid,
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
            request.investmentdate = Convert.ToDateTime(DateTime.Now.Year + request.investmentdate.ToString(".MM.dd HH:mm:ss"));

            cmd.CommandText(@"insert into investment
(entryid,
householdid,
investmentdate,
assetreceivedtypeid,
assetreceivedid,
quantity,
unitprice,
totalprice,
note,
updatedby)
values
(@entryid,
@householdid,
@investmentdate,
@assetreceivedtypeid,
@assetreceivedid,
@quantity,
@unitprice,
@totalprice,
@note,
@updatedby) 
on duplicate key update 
householdid=@householdid,
investmentdate=@investmentdate,
assetreceivedtypeid=@assetreceivedtypeid,
assetreceivedid=@assetreceivedid,
quantity=@quantity,
unitprice=@unitprice,
totalprice=@totalprice,
note=@note,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@investmentdate", DbType.DateTime, request.investmentdate, ParameterDirection.Input);
            cmd.AddParam("@assetreceivedtypeid", DbType.Int32, request.assetreceivedtypeid, ParameterDirection.Input);
            cmd.AddParam("@assetreceivedid", DbType.Int32, request.assetreceivedid, ParameterDirection.Input);
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
    othersupport.supportreceivedtypeid,
    supportreceivedtype.name supportreceivedtype,
    othersupport.name,
    othersupport.quantity,
    othersupport.unitprice,
    FORMAT(othersupport.unitprice,2) funitprice,
    othersupport.totalprice,
    FORMAT(othersupport.totalprice,2) ftotalprice,
    othersupport.sponsoringorganizationid,
    sponsoringorganization.name sponsoringorganization,
    household.name householdname,
    district.name districtname,
    household.section,
    DATE_FORMAT(othersupport.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    othersupport
left join household on household.householdid = othersupport.householdid
left join district on district.districtid = household.districtid
left join supportreceivedtype on supportreceivedtype.id = othersupport.supportreceivedtypeid
left join sponsoringorganization on sponsoringorganization.id = othersupport.sponsoringorganizationid
where (othersupport.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by othersupport.supportdate desc");
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
    supportreceivedtypeid,
    name,
    quantity,
    unitprice,
    totalprice,
    sponsoringorganizationid,
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
            request.supportdate = Convert.ToDateTime(DateTime.Now.Year + request.supportdate.ToString(".MM.dd HH:mm:ss"));

            cmd.CommandText(@"insert into othersupport
(entryid,
householdid,
supportdate,
supportreceivedtypeid,
name,
quantity,
unitprice,
totalprice,
sponsoringorganizationid,
updatedby)
values
(@entryid,
@householdid,
@supportdate,
@supportreceivedtypeid,
@name,
@quantity,
@unitprice,
@totalprice,
@sponsoringorganizationid,
@updatedby) 
on duplicate key update 
householdid=@householdid,
supportdate=@supportdate,
supportreceivedtypeid=@supportreceivedtypeid,
name=@name,
quantity=@quantity,
unitprice=@unitprice,
totalprice=@totalprice,
sponsoringorganizationid=@sponsoringorganizationid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@supportdate", DbType.DateTime, request.supportdate, ParameterDirection.Input);
            cmd.AddParam("@supportreceivedtypeid", DbType.Int32, request.supportreceivedtypeid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@quantity", DbType.Decimal, request.quantity, ParameterDirection.Input);
            cmd.AddParam("@unitprice", DbType.Decimal, request.unitprice, ParameterDirection.Input);
            cmd.AddParam("@totalprice", DbType.Decimal, request.totalprice, ParameterDirection.Input);
            cmd.AddParam("@sponsoringorganizationid", DbType.Int32, request.sponsoringorganizationid, ParameterDirection.Input);
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
    mediatedactivity.mediatedservicetypeid,
    mediatedservicetype.name mediatedservicetype,
    mediatedactivity.intermediaryorganizationid,
    intermediaryorganization.name intermediaryorganization,
    mediatedactivity.proxyserviceid,
    proxyservice.name proxyservice,
    mediatedactivity.memberid, 
    householdmember.name membername,
    district.name districtname,
    household.section,
    coach.name coachname,
    DATE_FORMAT(mediatedactivity.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    mediatedactivity
left join householdmember on householdmember.memberid = mediatedactivity.memberid
left join household on household.householdid = mediatedactivity.householdid
left join district on district.districtid = household.districtid
left join mediatedservicetype on mediatedservicetype.id = mediatedactivity.mediatedservicetypeid
left join intermediaryorganization on intermediaryorganization.id = mediatedactivity.intermediaryorganizationid
left join proxyservice on proxyservice.id = mediatedactivity.proxyserviceid
left join coach on coach.coachid = household.coachid
where (mediatedactivity.householdid = @householdid or 0 = @householdid)
  and (household.coachid = @coachid or 0 = @coachid)
order by mediatedactivity.mediateddate desc");
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
    mediatedservicetypeid,
    intermediaryorganizationid,
    proxyserviceid,
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
            request.mediateddate = Convert.ToDateTime(DateTime.Now.Year + request.mediateddate.ToString(".MM.dd HH:mm:ss"));

            cmd.CommandText(@"insert into mediatedactivity
(entryid,
householdid,
mediateddate,
mediatedservicetypeid,
intermediaryorganizationid,
proxyserviceid,
memberid, 
updatedby)
values
(@entryid,
@householdid,
@mediateddate,
@mediatedservicetypeid,
@intermediaryorganizationid,
@proxyserviceid,
@memberid, 
@updatedby) 
on duplicate key update 
householdid=@householdid,
mediateddate=@mediateddate,
mediatedservicetypeid=@mediatedservicetypeid,
intermediaryorganizationid=@intermediaryorganizationid,
proxyserviceid=@proxyserviceid,
memberid=@memberid, 
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@entryid", DbType.Int32, request.entryid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@mediateddate", DbType.DateTime, request.mediateddate, ParameterDirection.Input);
            cmd.AddParam("@mediatedservicetypeid", DbType.Int32, request.mediatedservicetypeid, ParameterDirection.Input);
            cmd.AddParam("@intermediaryorganizationid", DbType.Int32, request.intermediaryorganizationid, ParameterDirection.Input);
            cmd.AddParam("@proxyserviceid", DbType.String, request.proxyserviceid, ParameterDirection.Input);
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