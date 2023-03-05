using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using HouseHolds.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class HouseHoldsRepository : IHouseHoldsRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public HouseHoldsRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        #region household

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coachid"></param>
        /// <param name="status"></param>
        /// <param name="group"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public MResult GetHouseHoldList(int coachid, int status, int group, int districtid)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    household.numberof,
    household.name,
    household.householdgroupid,
    district.name districtname,
    household.section,
    household.address,
    household.phone,
    householdstatus.name householdstatus,
    household.coachid
FROM
    household
left join district on district.districtid = household.districtid
left join householdstatus on householdstatus.id = household.status
where (household.coachid = @coachid or 0 = @coachid) 
and household.status = @status 
and (household.householdgroupid = @group or 0 = @group)
and (household.districtid = @districtid or 0 = @districtid)
order by household.updated desc");
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            cmd.AddParam("@status", DbType.Int32, status, ParameterDirection.Input);
            cmd.AddParam("@group", DbType.Int32, group, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="districtid"></param>
        /// <param name="coachid"></param>
        /// <returns></returns>
        public MResult GetHouseHoldLocation(int districtid, int coachid)
        {
            List<object> retdata = new List<object>();
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    household.householdid,
    district.name districtname,
    household.districtid,
    household.districtid ||'-'|| household.section districtsection,
    coach.name coachname,
    household.coachid,
    household.householdgroupid,
    household.name,
    household.latitude,
    household.longitude,
    household.status
FROM
    household
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    coach ON coach.coachid = household.coachid
where latitude is not null and longitude is not null
  and (0 = @districtid or household.districtid = @districtid)
  and (0 = @coachid or household.coachid = @coachid)");
            cmd.AddParam("@districtid", DbType.Int32, districtid, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, coachid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;
            if (result.retdata is DataTable data)
            {
                foreach (DataRow dr in data.Rows)
                {
                    retdata.Add(new Hashtable
                    {
                        { "householdid",  dr["householdid"] },
                        { "districtid",  dr["districtid"] },
                        { "districtname",  dr["districtname"] },
                        { "section",  dr["districtsection"] },
                        { "coachid",  dr["coachid"] },
                        { "coachname",  dr["coachname"] },
                        { "householdgroupid",  dr["householdgroupid"] },
                        { "center",  new Hashtable { { "latitude", dr["latitude"] }, { "longitude", dr["longitude"] } } },
                        { "options", new Hashtable { { "title", dr["name"] } } }
                    });
                }

            }
            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public MResult GetHouseHoldSurvey(surveyfilter filter)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(string.Format(@"SELECT 
    data.*,
    ROUND((data.h1 + data.h2 + data.h3) / 3, 1) r1,
    ROUND((data.h4 + data.h5) / 2, 1) r2,
    ROUND((data.h6 + data.h7) / 2, 1) r3,
    ROUND((data.h8 + data.h9 + data.h10 + data.h11 + data.h12 + data.h13) / 6,
            1) r4,
    ROUND((data.h1 + data.h2 + data.h3 + data.h4 + data.h5 + data.h6 + data.h7 + data.h8 + data.h9 + data.h10 + data.h11 + data.h12 + data.h13) / 13,
            1) r5,
80 r0
FROM
    (SELECT 
        householdsurvey.dugaar,
        'Асуумж '||dugaar name,
            COUNT(householdsurvey.householdid) householdcount,
            COALESCE(AVG(h1), 0) h1,
            COALESCE(AVG(h2), 0) h2,
            COALESCE(AVG(h3), 0) h3,
            COALESCE(AVG(h4), 0) h4,
            COALESCE(AVG(h5), 0) h5,
            COALESCE(AVG(h6), 0) h6,
            COALESCE(AVG(h7), 0) h7,
            COALESCE(AVG(h8), 0) h8,
            COALESCE(AVG(h9), 0) h9,
            COALESCE(AVG(h10), 0) h10,
            COALESCE(AVG(h11), 0) h11,
            COALESCE(AVG(h12), 0) h12,
            COALESCE(AVG(h13), 0) h13
    FROM
        household
    INNER JOIN householdsurvey ON householdsurvey.householdid = household.householdid
    LEFT JOIN district ON district.districtid = household.districtid
    LEFT JOIN coach ON coach.coachid = household.coachid
    LEFT JOIN householdgroup ON householdgroup.id = household.householdgroupid
    WHERE
        householdsurvey.dugaar IN ({0})
            AND household.status = @status
            AND (1 > @districtid
            OR household.districtid = @districtid)
            AND (1 > @section
            OR household.section = @section)
            AND (1 > @householdgroupid
            OR household.householdgroupid = @householdgroupid)
            AND (1 > @householdid
            OR household.householdid = @householdid)
    GROUP BY householdsurvey.dugaar) data", string.Join(",", filter.dugaar)));
            cmd.AddParam("@status", DbType.Int32, filter.status, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, filter.district ?? 0, ParameterDirection.Input);
            cmd.AddParam("@section", DbType.Int32, filter.section ?? 0, ParameterDirection.Input);
            cmd.AddParam("@householdgroupid", DbType.Int32, filter.group ?? 0, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, filter.coach ?? 0, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, filter.household ?? 0, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseHold(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    household.householdid,
    (select count(1) from householdmember a where a.householdid = household.householdid) numberof,
    (select max(a.name) from householdmember a 
                     inner join relationship b 
                        on b.relationshipid = a.relationshipid 
                     where b.ishead = true 
                       and a.householdid = household.householdid) name,
    household.householdgroupid,
    householdgroup.name householdgroupname,
    household.districtid,
    district.name districtname,
    household.section,
    household.address,
    household.phone,
    household.status,
    household.coachid,
    DATE_FORMAT(household.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    household
left join district
on district.districtid = household.districtid
left join householdgroup on householdgroup.id = household.householdgroupid
where householdid = @householdid");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseHold(household request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.householdid == 0)
            {
                cmd.CommandText(@"select coalesce(max(householdid),0)+1 newid from household");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.householdid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into household
(householdid,
numberof,
name,
householdgroupid,
districtid,
section,
address,
phone,
status,
coachid,
updatedby)
values
(@householdid,
@numberof,
@name,
@householdgroupid,
@districtid,
@section,
@address,
@phone,
@status,
@coachid,
@updatedby) 
on duplicate key update 
numberof=@numberof,
name=@name,
householdgroupid=@householdgroupid,
districtid=@districtid,
section=@section,
address=@address,
phone=@phone,
status=@status,
coachid=@coachid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@numberof", DbType.Int32, request.numberof, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@householdgroupid", DbType.Int32, request.householdgroupid, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@section", DbType.Int32, request.section, ParameterDirection.Input);
            cmd.AddParam("@address", DbType.String, request.address, ParameterDirection.Input);
            cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            cmd.AddParam("@status", DbType.Int32, request.status, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, true);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.householdid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="householdid"></param>
        /// <param name="householdgroupid"></param>
        /// <returns></returns>
        public MResult SetHouseHoldGroup(int householdid, int householdgroupid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"UPDATE household 
SET 
    householdgroupid = @householdgroupid,
    updated = CURRENT_TIMESTAMP,
    updatedby = @updatedby
WHERE
    householdid = @householdid");

            cmd.AddParam("@householdid", DbType.Int32, householdid, ParameterDirection.Input);
            cmd.AddParam("@householdgroupid", DbType.Int32, householdgroupid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteHouseHold(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("select count(1) too from householdmember where householdid = @householdid");
            cmd.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн гишүүн бүртгэлд ашигласан тул устгах боломжгүй." };

            cmd.CommandText("delete from household where householdid = @householdid");
            return connector.Execute(ref cmd, true);

        }

        #endregion

        #region householdmember

        /// <summary>
        /// 
        /// </summary>
        /// <param name="householdid"></param>
        /// <returns></returns>
        public MResult GetHouseHoldMemberList(int householdid)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    householdmember.memberid,
    householdmember.householdid,
    householdmember.name,
    householdmember.relationshipid,
    relationship.name relationshipname,
    date_format(householdmember.birthdate, '%Y-%m-%d') birthdate,
    case
        when householdmember.gender = 0 then 'Эрэгтэй'
        else 'Эмэгтэй'
    end gender,
    case
        when householdmember.istogether = 0 then 'Үгүй'
        else 'Тийм'
    end istogether,
    case
        when householdmember.isparticipant = 0 then 'Үгүй'
        else 'Тийм'
    end isparticipant,
    householdmember.educationdegreeid,
    educationdegree.name educationdegree,
    householdmember.employmentstatusid,
    employmentstatus.name employmentstatus,
    householdmember.healthconditionid,
    healthcondition.name healthcondition,
    date_format(householdmember.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdmember 
left join relationship
on relationship.relationshipid = householdmember.relationshipid
left join educationdegree
on educationdegree.id = householdmember.educationdegreeid
left join employmentstatus
on employmentstatus.id = householdmember.employmentstatusid
left join healthcondition
on healthcondition.id = householdmember.healthconditionid
where householdmember.householdid = @householdid
order by householdmember.birthdate asc");
            cmd.AddParam("@householdid", DbType.Int32, householdid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseHoldMember(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"select
    memberid,
    householdid,
    name,
    relationshipid,
    date_format(birthdate, '%Y-%m-%d') birthdate,
    gender,
    istogether,
    isparticipant,
    educationdegreeid,
    employmentstatusid,
    healthconditionid,
    date_format(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdmember
where memberid = @memberid");
            cmd.AddParam("@memberid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseHoldMember(householdmember request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.memberid == 0)
            {
                cmd.CommandText(@"select coalesce(max(memberid),0)+1 newid from householdmember");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.memberid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into householdmember
(memberid,
householdid,
name,
relationshipid,
birthdate,
gender,
istogether,
isparticipant,
educationdegreeid,
employmentstatusid,
healthconditionid,
updatedby)
values
(@memberid,
@householdid,
@name,
@relationshipid,
@birthdate,
@gender,
@istogether,
@isparticipant,
@educationdegreeid,
@employmentstatusid,
@healthconditionid,
@updatedby) 
on duplicate key update 
householdid=@householdid,
name=@name,
relationshipid=@relationshipid,
birthdate=@birthdate,
gender=@gender,
istogether=@istogether,
isparticipant=@isparticipant,
educationdegreeid=@educationdegreeid,
employmentstatusid=@employmentstatusid,
healthconditionid=@healthconditionid,
updated=current_timestamp,
updatedby=@updatedby");

            cmd.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@relationshipid", DbType.Int32, request.relationshipid, ParameterDirection.Input);
            cmd.AddParam("@birthdate", DbType.DateTime, request.birthdate, ParameterDirection.Input);
            cmd.AddParam("@gender", DbType.Int32, request.gender, ParameterDirection.Input);
            cmd.AddParam("@istogether", DbType.Boolean, request.istogether, ParameterDirection.Input);
            cmd.AddParam("@isparticipant", DbType.Boolean, request.isparticipant, ParameterDirection.Input);
            cmd.AddParam("@educationdegreeid", DbType.Int32, request.educationdegreeid, ParameterDirection.Input);
            cmd.AddParam("@employmentstatusid", DbType.Int32, request.employmentstatusid, ParameterDirection.Input);
            cmd.AddParam("@healthconditionid", DbType.Int32, request.healthconditionid, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, true);
            if (result.rettype != 0)
                return result;

            cmd.CommandText(@"update household a set 
a.name = (select max(b.name) from householdmember b where b.householdid = a.householdid and b.isparticipant = true), 
a.numberof = (select count(1) from householdmember b where b.householdid = a.householdid)
where a.householdid = @householdid");
            cmd.ClearParam();
            cmd.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);

            result = connector.Execute(ref cmd, true);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.memberid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteHouseHoldMember(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from householdmember where memberid = @memberid");
            cmd.AddParam("@memberid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        #endregion
    }
}