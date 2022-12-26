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
        /// <returns></returns>
        public MResult GetHouseHoldList()
        {

            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    household.householdid,
    (select count(1) from householdmember a where a.householdid = household.householdid) numberof,
    (select max(a.name) from householdmember a 
                     inner join relationship b 
                        on b.relationshipid = a.relationshipid 
                     where b.ishead = true 
                       and a.householdid = household.householdid) name,
    household.district,
    district.name districtname,
    household.section,
    household.address,
    household.phone,
    household.coachid,
    coach.name coachname,
    DATE_FORMAT(household.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    household
left join district
on district.districtid = household.district
left join coach
on coach.coachid = household.coachid
order by household.updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseHold(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    householdid,
    numberof,
    name,
    district,
    section,
    address,
    phone,
    coachid,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    household
where householdid = @householdid");
            command.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseHold(household request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.householdid == 0)
            {
                command.CommandText(@"select coalesce(max(householdid),0)+1 newid from household");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.householdid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into household
(householdid,
numberof,
name,
district,
section,
address,
phone,
coachid,
updatedby)
values
(@householdid,
@numberof,
@name,
@district,
@section,
@address,
@phone,
@coachid,
@updatedby) 
on duplicate key update 
numberof=@numberof,
name=@name,
district=@district,
section=@section,
address=@address,
phone=@phone,
coachid=@coachid,
updated=current_timestamp,
updatedby=@updatedby");

            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@numberof", DbType.Int32, request.numberof, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@district", DbType.Int32, request.districtid, ParameterDirection.Input);
            command.AddParam("@section", DbType.Int32, request.section, ParameterDirection.Input);
            command.AddParam("@address", DbType.String, request.address, ParameterDirection.Input);
            command.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
            command.AddParam("@coachid", DbType.Int32, request.coachid, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.householdid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteHouseHold(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText("select count(1) too from householdmember where householdid = @householdid");
            command.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн гишүүн бүртгэлд ашигласан тул устгах боломжгүй." };

            command.CommandText("delete from household where householdid = @householdid");
            return connector.Execute(ref command, false);

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

            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
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
    householdmember.educationlevel,
    householdmember.employment,
    householdmember.health,
    date_format(householdmember.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdmember 
left join relationship
on relationship.relationshipid = householdmember.relationshipid
where householdmember.householdid = @householdid
order by householdmember.birthdate asc");
            command.AddParam("@householdid", DbType.Int32, householdid, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetHouseHoldMember(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"select
    memberid,
    householdid,
    name,
    relationshipid,
    date_format(birthdate, '%Y-%m-%d') birthdate,
    gender,
    istogether,
    educationlevel,
    employment,
    health,
    date_format(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdmember
where memberid = @memberid");
            command.AddParam("@memberid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetHouseHoldMember(householdmember request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.memberid == 0)
            {
                command.CommandText(@"select coalesce(max(memberid),0)+1 newid from householdmember");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.memberid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into householdmember
(memberid,
householdid,
name,
relationshipid,
birthdate,
gender,
istogether,
educationlevel,
employment,
health,
updatedby)
values
(@memberid,
@householdid,
@name,
@relationshipid,
@birthdate,
@gender,
@istogether,
@educationlevel,
@employment,
@health,
@updatedby) 
on duplicate key update 
householdid=@householdid,
name=@name,
relationshipid=@relationshipid,
birthdate=@birthdate,
gender=@gender,
istogether=@istogether,
educationlevel=@educationlevel,
employment=@employment,
health=@health,
updated=current_timestamp,
updatedby=@updatedby");

            command.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@relationshipid", DbType.Int32, request.relationshipid, ParameterDirection.Input);
            command.AddParam("@birthdate", DbType.DateTime, request.birthdate, ParameterDirection.Input);
            command.AddParam("@gender", DbType.Int32, request.gender, ParameterDirection.Input);
            command.AddParam("@istogether", DbType.Boolean, request.istogether, ParameterDirection.Input);
            command.AddParam("@educationlevel", DbType.String, request.educationlevel, ParameterDirection.Input);
            command.AddParam("@employment", DbType.String, request.employment, ParameterDirection.Input);
            command.AddParam("@health", DbType.String, request.health, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
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

            MCommand command = connector.PopCommand();
            command.CommandText("delete from householdmember where memberid = @memberid");
            command.AddParam("@memberid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        #endregion
    }
}