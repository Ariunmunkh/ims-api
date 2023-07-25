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
            using (DataSet ds = new DataSet())
            {
                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    committeereportdtl.*
FROM
    committeereportdtl
where committeereportdtl.reportid = @reportid");
                cmd.AddParam("@reportid", DbType.Int32, id, ParameterDirection.Input);
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable);
                ds.Tables[ds.Tables.Count - 1].TableName = "committeereportdtl";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    program.*
FROM program
order by program.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable);
                ds.Tables[ds.Tables.Count - 1].TableName = "program";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    agegroup.*
FROM agegroup
order by agegroup.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable);
                ds.Tables[ds.Tables.Count - 1].TableName = "agegroup";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    indicator.*
FROM indicator
order by indicator.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable);
                ds.Tables[ds.Tables.Count - 1].TableName = "indicator";

                ds.Tables.Add("retdata");
                ds.Tables["retdata"].Columns.Add("key", typeof(int));
                string col1, col2;
                foreach (DataRow dr in ds.Tables["agegroup"].Rows)
                {
                    ds.Tables["retdata"].Columns.Add(string.Format("male{0}", dr["id"]), typeof(int));
                    ds.Tables["retdata"].Columns.Add(string.Format("female{0}", dr["id"]), typeof(int));
                }
                DataRow newrow;
                DataRow[] rows;
                foreach (DataRow dr in ds.Tables["indicator"].Rows)
                {
                    newrow = ds.Tables["retdata"].NewRow();

                    newrow["key"] = dr["id"];

                    foreach (DataRow row in ds.Tables["agegroup"].Rows)
                    {
                        col1 = string.Format("male{0}", row["id"]);
                        col2 = string.Format("female{0}", row["id"]);
                        rows = ds.Tables["committeereportdtl"].Select("indicatorid = '" + dr["id"] + "' and agegroupid = '" + row["id"] + "'");
                        if (rows.Length > 0)
                        {
                            newrow[col1] = rows[0]["male"];
                            newrow[col2] = rows[0]["female"];
                        }
                        else
                        {
                            newrow[col1] = 0;
                            newrow[col2] = 0;
                        }
                    }
                    ds.Tables["retdata"].Rows.Add(newrow);
                }
                ds.Tables.Remove("committeereportdtl");
                ds.AcceptChanges();

                return new MResult { retdata = ds.Copy() };
            }
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
                cmd.CommandText(@"insert into committeereport
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

            if (request.dtls != null && request.dtls.Length > 0)
            {
                foreach (CommitteeReportDtl dtl in request.dtls)
                {
                    if (dtl.id == 0)
                    {
                        cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeereportdtl");
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                        if (result.retdata is DataTable data && data.Rows.Count > 0)
                        {
                            dtl.id = Convert.ToInt32(data.Rows[0]["newid"]);
                        }
                    }

                    cmd.CommandText("select count(1) too from committeereportdtl where reportid = @reportid and indicatorid = @indicatorid and agegroupid = @agegroupid");
                    cmd.ClearParam();
                    cmd.AddParam("@reportid", DbType.Int32, request.id, ParameterDirection.Input);
                    cmd.AddParam("@indicatorid", DbType.Int32, dtl.indicatorid, ParameterDirection.Input);
                    cmd.AddParam("@agegroupid", DbType.Int32, dtl.agegroupid, ParameterDirection.Input);

                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                    if (result.retdata is DataTable tdata && tdata.Rows.Count > 0 && Convert.ToDecimal(tdata.Rows[0][0]) > 0)
                    {
                        cmd.CommandText(@"update committeereportdtl set 
male=@male,
female=@female,
updated=current_timestamp, 
updatedby=@updatedby 
where reportid = @reportid 
and indicatorid = @indicatorid 
and agegroupid = @agegroupid");
                        cmd.ClearParam();
                        cmd.AddParam("@reportid", DbType.Int32, request.id, ParameterDirection.Input);
                        cmd.AddParam("@indicatorid", DbType.Int32, dtl.indicatorid, ParameterDirection.Input);
                        cmd.AddParam("@agegroupid", DbType.Int32, dtl.agegroupid, ParameterDirection.Input);

                        cmd.AddParam("@male", DbType.Int32, dtl.male, ParameterDirection.Input);
                        cmd.AddParam("@female", DbType.Int32, dtl.female, ParameterDirection.Input);
                        cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                    }
                    else
                    {
                        cmd.CommandText(@"insert into committeereportdtl
(id,
reportid,
programid,
indicatorid,
agegroupid,
male,
female,
updatedby)
values
(@id,
@reportid,
@programid,
@indicatorid,
@agegroupid,
@male,
@female,
@updatedby) 
on duplicate key update 
reportid=@reportid,
programid=@programid,
indicatorid=@indicatorid,
agegroupid=@agegroupid,
male=@male,
female=@female,
updated=current_timestamp,
updatedby=@updatedby");
                        cmd.ClearParam();
                        cmd.AddParam("@id", DbType.Int32, dtl.id, ParameterDirection.Input);
                        cmd.AddParam("@reportid", DbType.Int32, request.id, ParameterDirection.Input);
                        cmd.AddParam("@programid", DbType.Int32, dtl.programid, ParameterDirection.Input);
                        cmd.AddParam("@indicatorid", DbType.Int32, dtl.indicatorid, ParameterDirection.Input);
                        cmd.AddParam("@agegroupid", DbType.Int32, dtl.agegroupid, ParameterDirection.Input);
                        cmd.AddParam("@male", DbType.Int32, dtl.male, ParameterDirection.Input);
                        cmd.AddParam("@female", DbType.Int32, dtl.female, ParameterDirection.Input);
                        cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                    }
                }
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