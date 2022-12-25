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
    public class BaseRepository : IBaseRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public BaseRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        #region District

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetDistrictList()
        {

            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    districtid,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    district
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetDistrict(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    districtid,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    district
where districtid = @districtid");
            command.AddParam("@districtid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetDistrict(district request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.districtid == 0)
            {
                command.CommandText(@"select coalesce(max(districtid),0)+1 newid from district");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.districtid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into district
(districtid,
name,
updatedby)
values
(@districtid,
@name,
@updatedby) 
on duplicate key update 
name=@name,
updated=current_timestamp,
updatedby=@updatedby");

            command.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.districtid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteDistrict(int id)
        {

            MCommand command = connector.PopCommand();
            command.CommandText("select count(1) too from household where district = @districtid");
            command.AddParam("@districtid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн бүртгэлд ашигласан тул устгах боломжгүй." };

            command.CommandText("delete from district where districtid = @districtid");
            return connector.Execute(ref command, false);

        }

        #endregion


        #region Relationship

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetRelationshipList()
        {

            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    relationshipid,
    name,
    case
        when ishead = 0 then 'Гишүүн'
        else 'Өрхийн тэргүүн'
    end ishead,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    relationship
order by updated desc");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetRelationship(int id)
        {
            MCommand command = connector.PopCommand();
            command.CommandText(@"SELECT 
    relationshipid,
    name,
    ishead,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    relationship
where relationshipid = @relationshipid");
            command.AddParam("@relationshipid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetRelationship(relationship request)
        {

            MCommand command = connector.PopCommand();
            MResult result;

            if (request.relationshipid == 0)
            {
                command.CommandText(@"select coalesce(max(relationshipid),0)+1 newid from relationship");
                result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.relationshipid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(@"insert into relationship
(relationshipid,
name,
ishead,
updatedby)
values
(@relationshipid,
@name,
@ishead,
@updatedby) 
on duplicate key update 
name=@name,
ishead=@ishead,
updated=current_timestamp,
updatedby=@updatedby");

            command.AddParam("@relationshipid", DbType.Int32, request.relationshipid, ParameterDirection.Input);
            command.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            command.AddParam("@ishead", DbType.Boolean, request.ishead, ParameterDirection.Input);
            command.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            return new MResult { retdata = request.relationshipid };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteRelationship(int id)
        {

            MCommand command = connector.PopCommand();
            command.CommandText("select count(1) too from householdmember where relationshipid = @relationshipid");
            command.AddParam("@relationshipid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref command, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн гишүүн бүртгэлд ашигласан тул устгах боломжгүй." };

            command.CommandText("delete from relationship where relationshipid = @relationshipid");
            return connector.Execute(ref command, false);

        }

        #endregion
    }
}