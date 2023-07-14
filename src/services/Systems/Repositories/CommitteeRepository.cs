using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using Systems.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public CommitteeRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetRepoertList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeereport.*
FROM
    committeereport
order by reportdate desc");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetRepoert(int id)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeereportdtl.*
FROM
    committeereportdtl
where committeereportdtl.reportid = @reportid
order by agegroupid");
            cmd.AddParam("@reportid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetReport(CommitteeReport request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeereport");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from committeereport where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update committeereport set 
committeeid=@committeeid,
reportdate=@reportdate,
updated=current_timestamp, 
updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@reportdate", DbType.DateTime, request.reportdate, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into committee
(id,
committeeid,
reportdate,
updatedby)
values
(@id,
@committeeid,
@reportdate,
@updatedby) 
on duplicate key update 
committeeid=@committeeid,
reportdate=@reportdate,
updated=current_timestamp,
updatedby=@updatedby");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.String, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@reportdate", DbType.DateTime, request.reportdate, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }

            return new MResult { retdata = request.id };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteReport(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from committee where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

    }
}