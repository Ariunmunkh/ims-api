﻿using BaseLibrary.LConnection;
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
        public MResult GetRepoertList(int committeeid)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeereport.id,
    committeereport.committeeid,
    committee.name committee,
    DATE_FORMAT(committeereport.reportdate, '%Y-%m') reportdate,
    DATE_FORMAT(committeereport.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    committeereport
left join committee
on committee.id = committeereport.committeeid
where (committeereport.committeeid = @committeeid or 0 = @committeeid)
order by reportdate desc");
            cmd.AddParam("@committeeid", DbType.Int32, committeeid, ParameterDirection.Input);
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

        #region LocalInfo

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetLocalInfoList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT *
FROM localinfo");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetLocalInfo(int id)
        {
            using (DataSet ds = new DataSet())
            {
                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    localinfo.*
FROM
    localinfo
where localinfo.committeeid = @committeeid");
                cmd.AddParam("@committeeid", DbType.Int32, id, ParameterDirection.Input);
                return connector.Execute(ref cmd, false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetLocalInfo(LocalInfo request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from localinfo");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from localinfo where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update localinfo set 
committeeid =@committeeid,
c1_1=@c1_1,
c1_2=@c1_2,
c1_3=@c1_3,
c1_4=@c1_4,
c1_4_1=@c1_4_1,
c1_4_2=@c1_4_2,
c1_4_3=@c1_4_3,
c1_4_4=@c1_4_4,
c1_4_5=@c1_4_5,
c1_4_6=@c1_4_6,
c1_5=@c1_5,
c1_6=@c1_6,
c1_6_1=@c1_6_1,
c1_6_2=@c1_6_2,
c1_6_3=@c1_6_3,
c1_6_4=@c1_6_4,
c1_6_5=@c1_6_5,
c1_6_6=@c1_6_6,
c1_7=@c1_7,
c1_8=@c1_8,
c1_8_1=@c1_8_1,
c1_8_2=@c1_8_2,
c1_8_3=@c1_8_3,
c1_8_4=@c1_8_4,
c1_8_5=@c1_8_5,
c1_9=@c1_9,
c1_10=@c1_10,
c1_11=@c1_11,
c1_12=@c1_12,
c1_13=@c1_13,
c1_14=@c1_14,
c1_15=@c1_15,
c1_16=@c1_16,

updated=current_timestamp, 
updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c1_1", DbType.String, request.c1_1, ParameterDirection.Input);
                cmd.AddParam("@c1_2", DbType.String, request.c1_2, ParameterDirection.Input);
                cmd.AddParam("@c1_3", DbType.String, request.c1_3, ParameterDirection.Input);
                cmd.AddParam("@c1_4", DbType.String, request.c1_4, ParameterDirection.Input);
                cmd.AddParam("@c1_4_1", DbType.String, request.c1_4_1, ParameterDirection.Input);
                cmd.AddParam("@c1_4_2", DbType.String, request.c1_4_2, ParameterDirection.Input);
                cmd.AddParam("@c1_4_3", DbType.String, request.c1_4_3, ParameterDirection.Input);
                cmd.AddParam("@c1_4_4", DbType.String, request.c1_4_4, ParameterDirection.Input);
                cmd.AddParam("@c1_4_5", DbType.String, request.c1_4_5, ParameterDirection.Input);
                cmd.AddParam("@c1_4_6", DbType.String, request.c1_4_6, ParameterDirection.Input);
                cmd.AddParam("@c1_5", DbType.String, request.c1_5, ParameterDirection.Input);
                cmd.AddParam("@c1_6", DbType.String, request.c1_6, ParameterDirection.Input);
                cmd.AddParam("@c1_6_1", DbType.String, request.c1_6_1, ParameterDirection.Input);
                cmd.AddParam("@c1_6_2", DbType.String, request.c1_6_2, ParameterDirection.Input);
                cmd.AddParam("@c1_6_3", DbType.String, request.c1_6_3, ParameterDirection.Input);
                cmd.AddParam("@c1_6_4", DbType.String, request.c1_6_4, ParameterDirection.Input);
                cmd.AddParam("@c1_6_5", DbType.String, request.c1_6_5, ParameterDirection.Input);
                cmd.AddParam("@c1_6_6", DbType.String, request.c1_6_6, ParameterDirection.Input);
                cmd.AddParam("@c1_7", DbType.String, request.c1_7, ParameterDirection.Input);
                cmd.AddParam("@c1_8", DbType.String, request.c1_8, ParameterDirection.Input);
                cmd.AddParam("@c1_8_1", DbType.String, request.c1_8_1, ParameterDirection.Input);
                cmd.AddParam("@c1_8_2", DbType.String, request.c1_8_2, ParameterDirection.Input);
                cmd.AddParam("@c1_8_3", DbType.String, request.c1_8_3, ParameterDirection.Input);
                cmd.AddParam("@c1_8_4", DbType.String, request.c1_8_4, ParameterDirection.Input);
                cmd.AddParam("@c1_8_5", DbType.String, request.c1_8_5, ParameterDirection.Input);
                cmd.AddParam("@c1_9", DbType.String, request.c1_9, ParameterDirection.Input);
                cmd.AddParam("@c1_10", DbType.String, request.c1_10, ParameterDirection.Input);
                cmd.AddParam("@c1_11", DbType.String, request.c1_11, ParameterDirection.Input);
                cmd.AddParam("@c1_12", DbType.String, request.c1_12, ParameterDirection.Input);
                cmd.AddParam("@c1_13", DbType.String, request.c1_13, ParameterDirection.Input);
                cmd.AddParam("@c1_14", DbType.String, request.c1_14, ParameterDirection.Input);
                cmd.AddParam("@c1_15", DbType.String, request.c1_15, ParameterDirection.Input);
                cmd.AddParam("@c1_16", DbType.String, request.c1_16, ParameterDirection.Input);

                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into localinfo
(id,
committeeid,
c1_1,
c1_2,
c1_3,
c1_4,
c1_4_1,
c1_4_2,
c1_4_3,
c1_4_4,
c1_4_5,
c1_4_6,
c1_5,
c1_6,
c1_6_1,
c1_6_2,
c1_6_3,
c1_6_4,
c1_6_5,
c1_6_6,
c1_7,
c1_8,
c1_8_1,
c1_8_2,
c1_8_3,
c1_8_4,
c1_8_5,
c1_9,
c1_10,
c1_11,
c1_12,
c1_13,
c1_14,
c1_15,
c1_16,
updatedby)
values
(@id,
@committeeid,
@c1_1,
@c1_2,
@c1_3,
@c1_4,
@c1_4_1,
@c1_4_2,
@c1_4_3,
@c1_4_4,
@c1_4_5,
@c1_4_6,
@c1_5,
@c1_6,
@c1_6_1,
@c1_6_2,
@c1_6_3,
@c1_6_4,
@c1_6_5,
@c1_6_6,
@c1_7,
@c1_8,
@c1_8_1,
@c1_8_2,
@c1_8_3,
@c1_8_4,
@c1_8_5,
@c1_9,
@c1_10,
@c1_11,
@c1_12,
@c1_13,
@c1_14,
@c1_15,
@c1_16,
@updatedby)");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c1_1", DbType.String, request.c1_1, ParameterDirection.Input);
                cmd.AddParam("@c1_2", DbType.String, request.c1_2, ParameterDirection.Input);
                cmd.AddParam("@c1_3", DbType.String, request.c1_3, ParameterDirection.Input);
                cmd.AddParam("@c1_4", DbType.String, request.c1_4, ParameterDirection.Input);
                cmd.AddParam("@c1_4_1", DbType.String, request.c1_4_1, ParameterDirection.Input);
                cmd.AddParam("@c1_4_2", DbType.String, request.c1_4_2, ParameterDirection.Input);
                cmd.AddParam("@c1_4_3", DbType.String, request.c1_4_3, ParameterDirection.Input);
                cmd.AddParam("@c1_4_4", DbType.String, request.c1_4_4, ParameterDirection.Input);
                cmd.AddParam("@c1_4_5", DbType.String, request.c1_4_5, ParameterDirection.Input);
                cmd.AddParam("@c1_4_6", DbType.String, request.c1_4_6, ParameterDirection.Input);
                cmd.AddParam("@c1_5", DbType.String, request.c1_5, ParameterDirection.Input);
                cmd.AddParam("@c1_6", DbType.String, request.c1_6, ParameterDirection.Input);
                cmd.AddParam("@c1_6_1", DbType.String, request.c1_6_1, ParameterDirection.Input);
                cmd.AddParam("@c1_6_2", DbType.String, request.c1_6_2, ParameterDirection.Input);
                cmd.AddParam("@c1_6_3", DbType.String, request.c1_6_3, ParameterDirection.Input);
                cmd.AddParam("@c1_6_4", DbType.String, request.c1_6_4, ParameterDirection.Input);
                cmd.AddParam("@c1_6_5", DbType.String, request.c1_6_5, ParameterDirection.Input);
                cmd.AddParam("@c1_6_6", DbType.String, request.c1_6_6, ParameterDirection.Input);
                cmd.AddParam("@c1_7", DbType.String, request.c1_7, ParameterDirection.Input);
                cmd.AddParam("@c1_8", DbType.String, request.c1_8, ParameterDirection.Input);
                cmd.AddParam("@c1_8_1", DbType.String, request.c1_8_1, ParameterDirection.Input);
                cmd.AddParam("@c1_8_2", DbType.String, request.c1_8_2, ParameterDirection.Input);
                cmd.AddParam("@c1_8_3", DbType.String, request.c1_8_3, ParameterDirection.Input);
                cmd.AddParam("@c1_8_4", DbType.String, request.c1_8_4, ParameterDirection.Input);
                cmd.AddParam("@c1_8_5", DbType.String, request.c1_8_5, ParameterDirection.Input);
                cmd.AddParam("@c1_9", DbType.String, request.c1_9, ParameterDirection.Input);
                cmd.AddParam("@c1_10", DbType.String, request.c1_10, ParameterDirection.Input);
                cmd.AddParam("@c1_11", DbType.String, request.c1_11, ParameterDirection.Input);
                cmd.AddParam("@c1_12", DbType.String, request.c1_12, ParameterDirection.Input);
                cmd.AddParam("@c1_13", DbType.String, request.c1_13, ParameterDirection.Input);
                cmd.AddParam("@c1_14", DbType.String, request.c1_14, ParameterDirection.Input);
                cmd.AddParam("@c1_15", DbType.String, request.c1_15, ParameterDirection.Input);
                cmd.AddParam("@c1_16", DbType.String, request.c1_16, ParameterDirection.Input);
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
        public MResult DeleteLocalInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from localinfo where id = @id");
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

        #region CommitteeInfo

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetCommitteeInfoList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT *
FROM committeeinfo");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetCommitteeInfo(int id)
        {
            using (DataSet ds = new DataSet())
            {
                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    committeeinfo.*
FROM
    committeeinfo
where committeeinfo.committeeid = @committeeid");
                cmd.AddParam("@committeeid", DbType.Int32, id, ParameterDirection.Input);
                return connector.Execute(ref cmd, false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetCommitteeInfo(CommitteeInfo request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeeinfo");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from committeeinfo where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update committeeinfo set 
committeeid=@committeeid,
c2_1 =@c2_1,
c2_2 =@c2_2,
c2_3 =@c2_3,
c2_4 =@c2_4,
c2_5 =@c2_5,
c2_6 =@c2_6,
c2_7 =@c2_7,
c2_8 =@c2_8,
c2_9 =@c2_9,
c2_9_1 =@c2_9_1,
c2_9_2 =@c2_9_2,
c2_9_3 =@c2_9_3,
c2_9_4 =@c2_9_4,
c2_10 =@c2_10,
c2_11 =@c2_11,
c2_11_1 =@c2_11_1,
c2_11_2 =@c2_11_2,
c2_11_3 =@c2_11_3,
c2_11_4 =@c2_11_4,
c2_12_1 =@c2_12_1,
c2_12_2 =@c2_12_2,
c2_12_3 =@c2_12_3,
c2_12_4 =@c2_12_4,
c2_13_1 =@c2_13_1,
c2_13_2 =@c2_13_2,
c2_13_3 =@c2_13_3,
c2_13_4 =@c2_13_4,
c2_13_5 =@c2_13_5,
c2_14_1 =@c2_14_1,
c2_14_2 =@c2_14_2,
c2_14_3 =@c2_14_3,
c2_15_1 =@c2_15_1,
c2_15_2 =@c2_15_2,
c2_15_3 =@c2_15_3,
c2_16 =@c2_16,

updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c2_1", DbType.String, request.c2_1, ParameterDirection.Input);
                cmd.AddParam("@c2_2", DbType.String, request.c2_2, ParameterDirection.Input);
                cmd.AddParam("@c2_3", DbType.String, request.c2_3, ParameterDirection.Input);
                cmd.AddParam("@c2_4", DbType.String, request.c2_4, ParameterDirection.Input);
                cmd.AddParam("@c2_5", DbType.String, request.c2_5, ParameterDirection.Input);
                cmd.AddParam("@c2_6", DbType.String, request.c2_6, ParameterDirection.Input);
                cmd.AddParam("@c2_7", DbType.String, request.c2_7, ParameterDirection.Input);
                cmd.AddParam("@c2_8", DbType.String, request.c2_8, ParameterDirection.Input);
                cmd.AddParam("@c2_9", DbType.String, request.c2_9, ParameterDirection.Input);
                cmd.AddParam("@c2_9_1", DbType.String, request.c2_9_1, ParameterDirection.Input);
                cmd.AddParam("@c2_9_2", DbType.String, request.c2_9_2, ParameterDirection.Input);
                cmd.AddParam("@c2_9_3", DbType.String, request.c2_9_3, ParameterDirection.Input);
                cmd.AddParam("@c2_9_4", DbType.String, request.c2_9_4, ParameterDirection.Input);
                cmd.AddParam("@c2_10", DbType.String, request.c2_10, ParameterDirection.Input);
                cmd.AddParam("@c2_11", DbType.String, request.c2_11, ParameterDirection.Input);
                cmd.AddParam("@c2_11_1", DbType.String, request.c2_11_1, ParameterDirection.Input);
                cmd.AddParam("@c2_11_2", DbType.String, request.c2_11_2, ParameterDirection.Input);
                cmd.AddParam("@c2_11_3", DbType.String, request.c2_11_3, ParameterDirection.Input);
                cmd.AddParam("@c2_11_4", DbType.String, request.c2_11_4, ParameterDirection.Input);
                cmd.AddParam("@c2_12_1", DbType.String, request.c2_12_1, ParameterDirection.Input);
                cmd.AddParam("@c2_12_2", DbType.String, request.c2_12_2, ParameterDirection.Input);
                cmd.AddParam("@c2_12_3", DbType.String, request.c2_12_3, ParameterDirection.Input);
                cmd.AddParam("@c2_12_4", DbType.String, request.c2_12_4, ParameterDirection.Input);
                cmd.AddParam("@c2_13_1", DbType.String, request.c2_13_1, ParameterDirection.Input);
                cmd.AddParam("@c2_13_2", DbType.String, request.c2_13_2, ParameterDirection.Input);
                cmd.AddParam("@c2_13_3", DbType.String, request.c2_13_3, ParameterDirection.Input);
                cmd.AddParam("@c2_13_4", DbType.String, request.c2_13_4, ParameterDirection.Input);
                cmd.AddParam("@c2_13_5", DbType.String, request.c2_13_5, ParameterDirection.Input);
                cmd.AddParam("@c2_14_1", DbType.String, request.c2_14_1, ParameterDirection.Input);
                cmd.AddParam("@c2_14_2", DbType.String, request.c2_14_2, ParameterDirection.Input);
                cmd.AddParam("@c2_14_3", DbType.String, request.c2_14_3, ParameterDirection.Input);
                cmd.AddParam("@c2_15_1", DbType.String, request.c2_15_1, ParameterDirection.Input);
                cmd.AddParam("@c2_15_2", DbType.String, request.c2_15_2, ParameterDirection.Input);
                cmd.AddParam("@c2_15_3", DbType.String, request.c2_15_3, ParameterDirection.Input);
                cmd.AddParam("@c2_16", DbType.String, request.c2_16, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into committeeinfo
(id,
committeeid,
c2_1,
c2_2,
c2_3,
c2_4,
c2_5,
c2_6,
c2_7,
c2_8,
c2_9,
c2_9_1,
c2_9_2,
c2_9_3,
c2_9_4,
c2_10,
c2_11,
c2_11_1,
c2_11_2,
c2_11_3,
c2_11_4,
c2_12_1,
c2_12_2,
c2_12_3,
c2_12_4,
c2_13_1,
c2_13_2,
c2_13_3,
c2_13_4,
c2_13_5,
c2_14_1,
c2_14_2,
c2_14_3,
c2_15_1,
c2_15_2,
c2_15_3,
c2_16,
updatedby)
values
(@id,
@committeeid,
@c2_1,
@c2_2,
@c2_3,
@c2_4,
@c2_5,
@c2_6,
@c2_7,
@c2_8,
@c2_9,
@c2_9_1,
@c2_9_2,
@c2_9_3,
@c2_9_4,
@c2_10,
@c2_11,
@c2_11_1,
@c2_11_2,
@c2_11_3,
@c2_11_4,
@c2_12_1,
@c2_12_2,
@c2_12_3,
@c2_12_4,
@c2_13_1,
@c2_13_2,
@c2_13_3,
@c2_13_4,
@c2_13_5,
@c2_14_1,
@c2_14_2,
@c2_14_3,
@c2_15_1,
@c2_15_2,
@c2_15_3,
@c2_16,
@updatedby)");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.String, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c2_1", DbType.String, request.c2_1, ParameterDirection.Input);
                cmd.AddParam("@c2_2", DbType.String, request.c2_2, ParameterDirection.Input);
                cmd.AddParam("@c2_3", DbType.String, request.c2_3, ParameterDirection.Input);
                cmd.AddParam("@c2_4", DbType.String, request.c2_4, ParameterDirection.Input);
                cmd.AddParam("@c2_5", DbType.String, request.c2_5, ParameterDirection.Input);
                cmd.AddParam("@c2_6", DbType.String, request.c2_6, ParameterDirection.Input);
                cmd.AddParam("@c2_7", DbType.String, request.c2_7, ParameterDirection.Input);
                cmd.AddParam("@c2_8", DbType.String, request.c2_8, ParameterDirection.Input);
                cmd.AddParam("@c2_9", DbType.String, request.c2_9, ParameterDirection.Input);
                cmd.AddParam("@c2_9_1", DbType.String, request.c2_9_1, ParameterDirection.Input);
                cmd.AddParam("@c2_9_2", DbType.String, request.c2_9_2, ParameterDirection.Input);
                cmd.AddParam("@c2_9_3", DbType.String, request.c2_9_3, ParameterDirection.Input);
                cmd.AddParam("@c2_9_4", DbType.String, request.c2_9_4, ParameterDirection.Input);
                cmd.AddParam("@c2_10", DbType.String, request.c2_10, ParameterDirection.Input);
                cmd.AddParam("@c2_11", DbType.String, request.c2_11, ParameterDirection.Input);
                cmd.AddParam("@c2_11_1", DbType.String, request.c2_11_1, ParameterDirection.Input);
                cmd.AddParam("@c2_11_2", DbType.String, request.c2_11_2, ParameterDirection.Input);
                cmd.AddParam("@c2_11_3", DbType.String, request.c2_11_3, ParameterDirection.Input);
                cmd.AddParam("@c2_11_4", DbType.String, request.c2_11_4, ParameterDirection.Input);
                cmd.AddParam("@c2_12_1", DbType.String, request.c2_12_1, ParameterDirection.Input);
                cmd.AddParam("@c2_12_2", DbType.String, request.c2_12_2, ParameterDirection.Input);
                cmd.AddParam("@c2_12_3", DbType.String, request.c2_12_3, ParameterDirection.Input);
                cmd.AddParam("@c2_12_4", DbType.String, request.c2_12_4, ParameterDirection.Input);
                cmd.AddParam("@c2_13_1", DbType.String, request.c2_13_1, ParameterDirection.Input);
                cmd.AddParam("@c2_13_2", DbType.String, request.c2_13_2, ParameterDirection.Input);
                cmd.AddParam("@c2_13_3", DbType.String, request.c2_13_3, ParameterDirection.Input);
                cmd.AddParam("@c2_13_4", DbType.String, request.c2_13_4, ParameterDirection.Input);
                cmd.AddParam("@c2_13_5", DbType.String, request.c2_13_5, ParameterDirection.Input);
                cmd.AddParam("@c2_14_1", DbType.String, request.c2_14_1, ParameterDirection.Input);
                cmd.AddParam("@c2_14_2", DbType.String, request.c2_14_2, ParameterDirection.Input);
                cmd.AddParam("@c2_14_3", DbType.String, request.c2_14_3, ParameterDirection.Input);
                cmd.AddParam("@c2_15_1", DbType.String, request.c2_15_1, ParameterDirection.Input);
                cmd.AddParam("@c2_15_2", DbType.String, request.c2_15_2, ParameterDirection.Input);
                cmd.AddParam("@c2_15_3", DbType.String, request.c2_15_3, ParameterDirection.Input);
                cmd.AddParam("@c2_16", DbType.String, request.c2_16, ParameterDirection.Input);
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
        public MResult DeleteCommitteeInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from committeeinfo where id = @id");
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