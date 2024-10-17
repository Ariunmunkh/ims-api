using BaseLibrary.LConnection;
using Connection.Model;
using DevExpress.Spreadsheet;
using LConnection.Model;
using System.Collections;
using System.Data;
using System.Xml.Linq;
using Systems.Models;

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
        public MResult GetRepoertExcel(int committeeid, DateTime reportdate)
        {
            try
            {
                MResult result = GetRepoert(committeeid, reportdate);
                if (result.rettype != 0)
                    return result;

                using DataSet ds = result.retdata as DataSet ?? new DataSet();

                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    committee.name
FROM committee
where committee.id = @committeeid");
                cmd.AddParam("@committeeid", DbType.Int32, committeeid, ParameterDirection.Input);
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable ?? new DataTable());
                ds.Tables[ds.Tables.Count - 1].TableName = "committee";
                string? committeename = string.Empty;
                if ((ds.Tables["committee"] ?? new()).Rows.Count > 0)
                {
                    committeename = (ds.Tables["committee"] ?? new()).Rows[0]["name"].ToString();
                }

                using Workbook workbook = new Workbook();

                try
                {
                    workbook.CreateNewDocument();
                    workbook.BeginUpdate();

                    Worksheet worksheet = workbook.Worksheets[0];
                    worksheet.Name = string.Format("{0:yyyy.MM} Сарын тоон мэдээлэл", reportdate);
                    int rowOffset = 0;
                    int columnOffset = 0;
                    string colmale;
                    string colfemale;
                    DataRow[] rows;

                    rowOffset++;
                    worksheet.Cells[rowOffset, 0].Value = (committeename ?? "Улаан загалмайн хороо").ToUpper();
                    worksheet.Cells[rowOffset, 0].Font.Bold = true;
                    rowOffset++;

                    rowOffset++;
                    worksheet.Cells[rowOffset, 0].Value = string.Format("Тайлангийн огноо: {0:yyyy.MM}", reportdate);
                    worksheet.Cells[rowOffset, 0].Font.Bold = true;
                    rowOffset++;

                    foreach (DataRow program in (ds.Tables["program"] ?? new()).Select("", "name"))
                    {
                        rowOffset++;
                        worksheet.Cells[rowOffset, 0].Value = program["name"].ToString();
                        worksheet.Cells[rowOffset, 0].Font.Bold = true;

                        rowOffset++;
                        foreach (DataRow indicator in (ds.Tables["indicator"] ?? new()).Select("headid = '" + program["id"] + "'", "name"))
                        {
                            rowOffset++;
                            worksheet.Cells[rowOffset, 0].Value = indicator["name"].ToString();
                            rowOffset++;
                            columnOffset = 0;
                            rows = (ds.Tables["retdata"] ?? new()).Select("key = '" + indicator["id"] + "'");
                            foreach (DataRow agedr in (ds.Tables["agegroup"] ?? new()).Select("", "name"))
                            {
                                worksheet.Cells[rowOffset, columnOffset].Value = agedr["name"].ToString();

                                worksheet.Cells[rowOffset + 1, columnOffset].Value = "эр";
                                worksheet.Cells[rowOffset + 1, columnOffset + 1].Value = "эм";

                                colmale = string.Format("male{0}", agedr["id"]);
                                colfemale = string.Format("female{0}", agedr["id"]);

                                if (rows.Length > 0 && rows[0].Table.Columns.Contains(colmale) && rows[0].Table.Columns.Contains(colfemale))
                                {
                                    worksheet.Cells[rowOffset + 2, columnOffset].Value = Convert.ToInt32(rows[0][colmale]);
                                    worksheet.Cells[rowOffset + 2, columnOffset + 1].Value = Convert.ToInt32(rows[0][colfemale]);
                                }
                                columnOffset += 2;
                            }
                            rowOffset += 2;
                        }
                        rowOffset++;
                    }

                }
                catch (Exception ex)
                {
                    return new MResult { retdata = ex, rettype = -1, retmsg = ex.Message };
                }
                finally
                {
                    workbook.EndUpdate();
                }





                using MemoryStream memory2 = new();

                workbook.SaveDocument(memory2, DocumentFormat.Xlsx);

                return new MResult { retdata = new Hashtable { { "file", Convert.ToBase64String(memory2.ToArray()) }, { "name", string.Format("{0} {1:yyyy.MM}.xlsx", committeename, reportdate) } } };

            }
            catch (Exception ex)
            {
                return new MResult { retdata = ex, rettype = -1, retmsg = ex.Message };
            }
        }
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
        /// <param name="committeeid"></param>
        /// <param name="reportdate"></param>
        /// <returns></returns>
        public MResult GetRepoert(int committeeid, DateTime reportdate)
        {
            using (DataSet ds = new DataSet())
            {
                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    committeereportdtl.*
FROM
    committeereportdtl
inner join committeereport on committeereport.id = committeereportdtl.reportid
where committeereport.committeeid = @committeeid and committeereport.reportdate = @reportdate");
                cmd.AddParam("@committeeid", DbType.Int32, committeeid, ParameterDirection.Input);
                cmd.AddParam("@reportdate", DbType.DateTime, reportdate, ParameterDirection.Input);
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable ?? new DataTable());
                ds.Tables[ds.Tables.Count - 1].TableName = "committeereportdtl";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    program.*
FROM program
order by program.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable ?? new DataTable());
                ds.Tables[ds.Tables.Count - 1].TableName = "program";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    agegroup.*
FROM agegroup
order by agegroup.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable ?? new DataTable());
                ds.Tables[ds.Tables.Count - 1].TableName = "agegroup";

                cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    indicator.*
FROM indicator
order by indicator.name");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                ds.Tables.Add(result.retdata as DataTable ?? new DataTable());
                ds.Tables[ds.Tables.Count - 1].TableName = "indicator";

                ds.Tables.Add("retdata");
                ds.Tables["retdata"]?.Columns.Add("key", typeof(int));
                string col1, col2;
                foreach (DataRow dr in (ds.Tables["agegroup"] ?? new DataTable()).Rows)
                {
                    ds.Tables["retdata"]?.Columns.Add(string.Format("male{0}", dr["id"]), typeof(int));
                    ds.Tables["retdata"]?.Columns.Add(string.Format("female{0}", dr["id"]), typeof(int));
                }
                DataRow newrow;
                DataRow[] rows;
                foreach (DataRow dr in (ds.Tables["indicator"] ?? new DataTable()).Rows)
                {
                    newrow = (ds.Tables["retdata"] ?? new DataTable()).NewRow();

                    newrow["key"] = dr["id"];

                    foreach (DataRow row in (ds.Tables["agegroup"] ?? new DataTable()).Rows)
                    {
                        col1 = string.Format("male{0}", row["id"]);
                        col2 = string.Format("female{0}", row["id"]);
                        rows = (ds.Tables["committeereportdtl"] ?? new DataTable()).Select("indicatorid = '" + dr["id"] + "' and agegroupid = '" + row["id"] + "'");
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
                    (ds.Tables["retdata"] ?? new DataTable()).Rows.Add(newrow);
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

            cmd.CommandText("select * from committeereport where committeeid = @committeeid and reportdate = @reportdate");
            cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
            cmd.AddParam("@reportdate", DbType.DateTime, request.reportdate, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                request.id = Convert.ToInt32(cdata.Rows[0]["id"]);
            }
            else
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeereport");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }

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
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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

                    cmd.CommandText("select * from committeereportdtl where reportid = @reportid and indicatorid = @indicatorid and agegroupid = @agegroupid");
                    cmd.ClearParam();
                    cmd.AddParam("@reportid", DbType.Int32, request.id, ParameterDirection.Input);
                    cmd.AddParam("@indicatorid", DbType.Int32, dtl.indicatorid, ParameterDirection.Input);
                    cmd.AddParam("@agegroupid", DbType.Int32, dtl.agegroupid, ParameterDirection.Input);

                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                    if (result.retdata is DataTable tdata && tdata.Rows.Count > 0)
                    {
                        dtl.id = Convert.ToInt32(tdata.Rows[0]["id"]);

                        cmd.CommandText(@"update committeereportdtl set 
male=@male,
female=@female,
updated=current_timestamp, 
updatedby=@updatedby 
where id = @id");
                        cmd.ClearParam();
                        cmd.AddParam("@id", DbType.Int32, dtl.id, ParameterDirection.Input);
                        cmd.AddParam("@reportid", DbType.Int32, request.id, ParameterDirection.Input);
                        cmd.AddParam("@indicatorid", DbType.Int32, dtl.indicatorid, ParameterDirection.Input);
                        cmd.AddParam("@agegroupid", DbType.Int32, dtl.agegroupid, ParameterDirection.Input);

                        cmd.AddParam("@male", DbType.Int32, dtl.male, ParameterDirection.Input);
                        cmd.AddParam("@female", DbType.Int32, dtl.female, ParameterDirection.Input);
                        cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
                        cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <param name="committeeid"></param>
        /// <returns></returns>
        public MResult GetRepoertInfoList(int committeeid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeereportinfo.*,
    committee.name committee,
    DATE_FORMAT(committeereportinfo.infodate, '%Y') infoyear,
    DATE_FORMAT(committeereportinfo.infodate, '%Y-%m-%d') infodate2
FROM
    committeereportinfo
left join committee
on committee.id = committeereportinfo.committeeid
where committeereportinfo.committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, committeeid, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            using DataTable data = result.retdata as DataTable ?? new DataTable();
            using DataTable yeardata = data.DefaultView.ToTable(true, "infoyear");
            List<object> retdata = new();
            List<object> children;
            foreach (DataRow year in yeardata.Select("", "infoyear desc"))
            {
                children = new();
                foreach (DataRow dr in data.Select("infoyear='" + year["infoyear"] + "'", "infodate2 desc"))
                {
                    children.Add(new Hashtable
                    {
                        { "title", dr["infodate2"] },
                        { "key", dr["id"] }
                    });
                }

                retdata.Add(new Hashtable
                {
                    { "title", year["infoyear"] },
                    { "key", year["infoyear"] },
                    { "children", children }
                });
            }
            return new MResult { retdata = retdata };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetRepoertInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeereportinfo.*
FROM
    committeereportinfo
where committeereportinfo.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetReportInfo(CommitteeReportInfo request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeereportinfo");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }
            if (!request.infodate.HasValue)
                request.infodate = DateTime.Now;

            cmd.CommandText(@"insert into committeereportinfo
(id,
committeeid,
info,
infodate,
updatedby)
values
(@id,
@committeeid,
@info,
@infodate,
@updatedby) 
on duplicate key update 
committeeid=@committeeid,
info=@info,
infodate=@infodate,
updated=current_timestamp,
updatedby=@updatedby");
            cmd.AddParam("@id", DbType.Int64, request.id, ParameterDirection.Input);
            cmd.AddParam("@committeeid", DbType.Int64, request.committeeid, ParameterDirection.Input);
            cmd.AddParam("@info", DbType.String, request.info, ParameterDirection.Input);
            cmd.AddParam("@infodate", DbType.DateTime, request.infodate, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult DeleteReportInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from committeereportinfo where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
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
            cmd.CommandText(@"SELECT 
    localinfo.*, committee.name committee
FROM
    localinfo
        LEFT JOIN
    committee ON committee.id = localinfo.committeeid");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetLocalInfo(int id)
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

                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
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
            cmd.CommandText(@"SELECT 
    committeeinfo.*, committee.name committee
FROM
    committeeinfo
        LEFT JOIN
    committee ON committee.id = committeeinfo.committeeid");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetCommitteeInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeeinfo.*
FROM
    committeeinfo
where committeeinfo.committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data)
            {
                if (data.Rows.Count > 0)
                {
                    data.Columns.Add("committeeinfodtl", typeof(object));

                    cmd.CommandText(@"SELECT 
    committeeinfodtl.*
FROM
    committeeinfodtl
where committeeinfodtl.committeeid = @committeeid");
                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                    data.Rows[0]["committeeinfodtl"] = result.retdata;
                }
                else
                {
                    DataRow newrow = data.NewRow();
                    newrow["id"] = 0;
                    newrow["committeeid"] = id;
                    data.Rows.Add(newrow);
                }
                return new MResult { retdata = data };
            }
            return new MResult { rettype = -1, retmsg = "data not found" };
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

            cmd.CommandText("select count(1) too from committeeinfo where committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update committeeinfo set 
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
where committeeid=@committeeid");
                cmd.ClearParam();
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
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }

            if (request.committeeinfodtl != null && request.committeeinfodtl.Length > 0)
            {
                cmd.CommandText("delete from committeeinfodtl where committeeid = @committeeid");
                cmd.ClearParam();
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                foreach (CommitteeInfoDtl dtl in request.committeeinfodtl)
                {
                    if (dtl.id < 1)
                    {
                        cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeeinfodtl");
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                        if (result.retdata is DataTable data && data.Rows.Count > 0)
                        {
                            dtl.id = Convert.ToInt32(data.Rows[0]["newid"]);
                        }
                    }

                    cmd.CommandText(@"insert into committeeinfodtl
(id,
committeeid,
name,
isnote,
isbank,
updatedby)
values
(@id,
@committeeid,
@name,
@isnote,
@isbank,
@updatedby)");
                    cmd.ClearParam();
                    cmd.AddParam("@id", DbType.Int32, dtl.id, ParameterDirection.Input);
                    cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                    cmd.AddParam("@name", DbType.String, dtl.name, ParameterDirection.Input);
                    cmd.AddParam("@isnote", DbType.Boolean, dtl.isnote, ParameterDirection.Input);
                    cmd.AddParam("@isbank", DbType.Boolean, dtl.isbank, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                }
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
            cmd.CommandText("delete from committeeinfo where committeeid = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            cmd.CommandText("delete from committeeinfodtl where committeeid = @id");
            result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

        #region committeeactivity

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetcommitteeactivityList()
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT *
FROM committeeactivity");
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult Getcommitteeactivity(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committeeactivity.*
FROM
    committeeactivity
where committeeactivity.committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data)
            {
                if (data.Rows.Count > 0)
                {
                    data.Columns.Add("committeeactivitydtl", typeof(object));

                    cmd.CommandText(@"SELECT 
    committeeactivitydtl.*
FROM
    committeeactivitydtl
where committeeactivitydtl.committeeid = @committeeid");
                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                    data.Rows[0]["committeeactivitydtl"] = result.retdata;
                }
                else
                {
                    DataRow newrow = data.NewRow();
                    newrow["id"] = 0;
                    newrow["committeeid"] = id;
                    data.Rows.Add(newrow);
                }
                return new MResult { retdata = data };
            }
            return new MResult { rettype = -1, retmsg = "data not found" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult Setcommitteeactivity(Committeeactivity request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeeactivity");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from committeeactivity where committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update committeeactivity set 
c3_3 =@c3_3,
c3_4 =@c3_4,
updatedby=@updatedby 
where committeeid=@committeeid");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c3_3", DbType.String, request.c3_3, ParameterDirection.Input);
                cmd.AddParam("@c3_4", DbType.String, request.c3_4, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into committeeactivity
(id,
committeeid,
c3_3,
c3_4,
updatedby)
values
(@id,
@committeeid,
@c3_3,
@c3_4,
@updatedby)");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c3_3", DbType.String, request.c3_3, ParameterDirection.Input);
                cmd.AddParam("@c3_4", DbType.String, request.c3_4, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }

            if (request.committeeactivitydtl != null && request.committeeactivitydtl.Length > 0)
            {
                cmd.CommandText("delete from committeeactivitydtl where committeeid = @committeeid");
                cmd.ClearParam();
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                foreach (Committeeactivitydtl dtl in request.committeeactivitydtl)
                {
                    if (dtl.id < 1)
                    {
                        cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committeeactivitydtl");
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                        if (result.retdata is DataTable data && data.Rows.Count > 0)
                        {
                            dtl.id = Convert.ToInt32(data.Rows[0]["newid"]);
                        }
                    }

                    cmd.CommandText(@"insert into committeeactivitydtl
(id,
committeeid,
name,
job,
type,
updatedby)
values
(@id,
@committeeid,
@name,
@job,
@type,
@updatedby)");
                    cmd.ClearParam();
                    cmd.AddParam("@id", DbType.Int32, dtl.id, ParameterDirection.Input);
                    cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                    cmd.AddParam("@name", DbType.String, dtl.name, ParameterDirection.Input);
                    cmd.AddParam("@job", DbType.String, dtl.job, ParameterDirection.Input);
                    cmd.AddParam("@type", DbType.Boolean, dtl.type, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;

                }
            }

            return new MResult { retdata = request.id };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult Deletecommitteeactivity(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from committeeactivity where committeeid = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            cmd.CommandText("delete from committeeactivitydtl where committeeid = @id");
            result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

        #region primarystageinfo

        /// <summary>
        /// 
        /// </summary>
        /// <param name="committeeid"></param>
        /// <returns></returns>
        public MResult GetPrimaryStageInfoList(int committeeid)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT *
FROM primarystageinfo 
where primarystageinfo.committeeid = @committeeid");
            cmd.AddParam("@committeeid", DbType.Int32, committeeid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetPrimaryStageInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    primarystageinfo.*
FROM
    primarystageinfo
where primarystageinfo.id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            using DataTable data = result.retdata as DataTable ?? new();

            if (data.Rows.Count > 0)
            {
                Hashtable retdata = new Hashtable();
                foreach (DataColumn dc in data.Columns)
                    retdata.Add(dc.Caption, data.Rows[0][dc.Caption]);
                return new MResult { retdata = retdata };
            }
            else
                return new MResult { rettype = -1, retmsg = "data not found" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetPrimaryStageInfo(PrimaryStageInfo request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from primarystageinfo");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from primarystageinfo where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update primarystageinfo set 
c4_1 =@c4_1,
c4_2 =@c4_2,
c4_3_1 =@c4_3_1,
c4_3_2 =@c4_3_2,
c4_4 =@c4_4,
c4_5 =@c4_5,
c4_6 =@c4_6,
c4_7 =@c4_7,
c4_8_1 =@c4_8_1,
c4_8_2 =@c4_8_2,
c4_8_3 =@c4_8_3,
c4_8_4 =@c4_8_4,
c4_9 =@c4_9,
c4_10 =@c4_10,
c4_11 =@c4_11,
committeeid=@committeeid,
updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);

                cmd.AddParam("@c4_1", DbType.String, request.c4_1, ParameterDirection.Input);
                cmd.AddParam("@c4_2", DbType.String, request.c4_2, ParameterDirection.Input);
                cmd.AddParam("@c4_3_1", DbType.String, request.c4_3_1, ParameterDirection.Input);
                cmd.AddParam("@c4_3_2", DbType.String, request.c4_3_2, ParameterDirection.Input);
                cmd.AddParam("@c4_4", DbType.String, request.c4_4, ParameterDirection.Input);
                cmd.AddParam("@c4_5", DbType.String, request.c4_5, ParameterDirection.Input);
                cmd.AddParam("@c4_6", DbType.String, request.c4_6, ParameterDirection.Input);
                cmd.AddParam("@c4_7", DbType.String, request.c4_7, ParameterDirection.Input);
                cmd.AddParam("@c4_8_1", DbType.String, request.c4_8_1, ParameterDirection.Input);
                cmd.AddParam("@c4_8_2", DbType.String, request.c4_8_2, ParameterDirection.Input);
                cmd.AddParam("@c4_8_3", DbType.String, request.c4_8_3, ParameterDirection.Input);
                cmd.AddParam("@c4_8_4", DbType.String, request.c4_8_4, ParameterDirection.Input);
                cmd.AddParam("@c4_9", DbType.String, request.c4_9, ParameterDirection.Input);
                cmd.AddParam("@c4_10", DbType.String, request.c4_10, ParameterDirection.Input);
                cmd.AddParam("@c4_11", DbType.String, request.c4_11, ParameterDirection.Input);

                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into primarystageinfo
(id,
committeeid,
c4_1,
c4_2,
c4_3_1,
c4_3_2,
c4_4,
c4_5,
c4_6,
c4_7,
c4_8_1,
c4_8_2,
c4_8_3,
c4_8_4,
c4_9,
c4_10,
c4_11,
updatedby)
values
(@id,
@committeeid,
@c4_1,
@c4_2,
@c4_3_1,
@c4_3_2,
@c4_4,
@c4_5,
@c4_6,
@c4_7,
@c4_8_1,
@c4_8_2,
@c4_8_3,
@c4_8_4,
@c4_9,
@c4_10,
@c4_11,
@updatedby)");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@committeeid", DbType.Int32, request.committeeid, ParameterDirection.Input);
                cmd.AddParam("@c4_1", DbType.String, request.c4_1, ParameterDirection.Input);
                cmd.AddParam("@c4_2", DbType.String, request.c4_2, ParameterDirection.Input);
                cmd.AddParam("@c4_3_1", DbType.String, request.c4_3_1, ParameterDirection.Input);
                cmd.AddParam("@c4_3_2", DbType.String, request.c4_3_2, ParameterDirection.Input);
                cmd.AddParam("@c4_4", DbType.String, request.c4_4, ParameterDirection.Input);
                cmd.AddParam("@c4_5", DbType.String, request.c4_5, ParameterDirection.Input);
                cmd.AddParam("@c4_6", DbType.String, request.c4_6, ParameterDirection.Input);
                cmd.AddParam("@c4_7", DbType.String, request.c4_7, ParameterDirection.Input);
                cmd.AddParam("@c4_8_1", DbType.String, request.c4_8_1, ParameterDirection.Input);
                cmd.AddParam("@c4_8_2", DbType.String, request.c4_8_2, ParameterDirection.Input);
                cmd.AddParam("@c4_8_3", DbType.String, request.c4_8_3, ParameterDirection.Input);
                cmd.AddParam("@c4_8_4", DbType.String, request.c4_8_4, ParameterDirection.Input);
                cmd.AddParam("@c4_9", DbType.String, request.c4_9, ParameterDirection.Input);
                cmd.AddParam("@c4_10", DbType.String, request.c4_10, ParameterDirection.Input);
                cmd.AddParam("@c4_11", DbType.String, request.c4_11, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, connector.RequestHeaderInfo.UserID, ParameterDirection.Input);

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
        public MResult DeletePrimaryStageInfo(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from primarystageinfo where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (!string.IsNullOrEmpty(result.retmsg) && result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;

                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

    }
}