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
order by householdid");
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
            command.AddParam("@district", DbType.String, request.district, ParameterDirection.Input);
            command.AddParam("@section", DbType.String, request.section, ParameterDirection.Input);
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
            command.CommandText("delete from household where householdid = @householdid");
            command.AddParam("@householdid", DbType.Int32, id, ParameterDirection.Input);
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
    memberid,
    householdid,
    name,
    relative,
    date_format(birthdate, '%Y-%m-%d') birthdate,
    case
        when gender = 0 then 'Эр'
        else 'Эм'
    end gender,
    case
        when istogether = 0 then 'Үгүй'
        else 'Тийм'
    end istogether,
    date_format(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    householdmember 
where householdid = @householdid
order by memberid");
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
    relative,
    date_format(birthdate, '%Y-%m-%d') birthdate,
    gender,
    istogether,
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
relative,
birthdate,
gender,
istogether,
updatedby )
values
(@memberid,
@householdid,
@name,
@relative,
@birthdate,
@gender,
@istogether,
@updatedby) 
on duplicate key update 
householdid=@householdid,
name=@name,
relative=@relative,
birthdate=@birthdate,
gender=@gender,
istogether=@istogether,
updated=current_timestamp,
updatedby=@updatedby");

            command.AddParam("@memberid", DbType.Int32, request.memberid, ParameterDirection.Input);
            command.AddParam("@householdid", DbType.Int32, request.householdid, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@relative", DbType.String, request.relative, ParameterDirection.Input);
            command.AddParam("@birthdate", DbType.DateTime, request.birthdate, ParameterDirection.Input);
            command.AddParam("@gender", DbType.Int32, request.gender, ParameterDirection.Input);
            command.AddParam("@istogether", DbType.Boolean, request.istogether, ParameterDirection.Input);
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