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

        #region DropDownItem

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetDropDownItemList(string type)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(string.Format(@"SELECT 
    {0}.*,
    '{0}' type,
    {0}.id value,
    {0}.name label,
    {0}.name text
FROM
    {0}
order by updated desc", type));
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public MResult GetDropDownItem(int id, string type)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(string.Format(@"SELECT 
    {0}.*,
    '{0}' type
FROM
    {0}
where id = @id", type));
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetDropDownItem(DropDownItem request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(string.Format(@"select coalesce(max(id),0)+1 newid from {0}", request.type));
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(string.Format("select * from {0} where id = @id", request.type));
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata)
            {
                bool ishead = cdata.Columns.Contains("headid");

                if (cdata.Rows.Count > 0 && Convert.ToInt32(cdata.Rows[0]["id"]) == request.id)
                {
                    cmd.CommandText(string.Format(@"update {0} set 
                                                           {1} 
                                                           name=@name, 
                                                           updated=current_timestamp, 
                                                           updatedby=@updatedby 
                                                     where id = @id", request.type, ishead ? "headid=@headid," : string.Empty));
                    cmd.ClearParam();
                    cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                    cmd.AddParam("@headid", DbType.Int32, request.headid, ParameterDirection.Input);
                    cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                    result = connector.Execute(ref cmd, false);
                    if (result.rettype != 0)
                        return result;
                }
                else
                {
                    cmd.CommandText(string.Format(@"insert into {0}
(id,
{1}
name,
updatedby)
values
(@id,
{2}
@name,
@updatedby) 
on duplicate key update 
name=@name,
updated=current_timestamp,
updatedby=@updatedby", request.type, ishead ? "headid," : string.Empty, ishead ? "@headid," : string.Empty));
                    cmd.ClearParam();
                    cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                    cmd.AddParam("@headid", DbType.Int32, request.headid, ParameterDirection.Input);
                    cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

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
        /// <param name="type"></param>
        /// <returns></returns>
        public MResult DeleteDropDownItem(int id, string type)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(string.Format("delete from {0} where id = @id", type));
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.retmsg.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
            {
                string tablename = string.Empty;
                if (result.retmsg.Contains("fk_householdmember_educationdegreeid")
                    || result.retmsg.Contains("fk_householdmember_employmentstatusid")
                    || result.retmsg.Contains("fk_householdmember_healthconditionid"))
                {
                    tablename = "Өрхийн гишүүдийн мэдээлэл";
                }
                else if (result.retmsg.Contains("fk_loan_loanpurposeid"))
                {
                    tablename = "Зээлийн мэдээлэл";
                }
                else if (result.retmsg.Contains("fk_training_trainingtypeid")
               || result.retmsg.Contains("fk_training_trainingandactivityid")
               || result.retmsg.Contains("fk_training_organizationid"))
                {
                    tablename = "Сургалт, үйл ажиллагаа";
                }
                else if (result.retmsg.Contains("fk_improvement_subbranchid"))
                {
                    tablename = "Амьжиргаа сайжруулах үйл ажиллагааны мэдээлэл";
                }
                else if (result.retmsg.Contains("fk_investment_assetreceivedtypeid")
               || result.retmsg.Contains("fk_investment_assetreceivedid"))
                {
                    tablename = "Хөрөнгө оруулалтын мэдээлэл";
                }
                else if (result.retmsg.Contains("fk_othersupport_supportreceivedtypeid")
               || result.retmsg.Contains("fk_othersupport_sponsoringorganizationid"))
                {
                    tablename = "Бусад тусламж, дэмжлэг";
                }
                else if (result.retmsg.Contains("fk_mediatedactivity_mediatedservicetypeid")
               || result.retmsg.Contains("fk_mediatedactivity_intermediaryorganizationid")
               || result.retmsg.Contains("fk_mediatedactivity_proxyserviceid"))
                {
                    tablename = "Холбон зуучилсан үйл ажиллагаа";
                }
                result.retmsg = string.Format("{0} бүртгэлд ашигласан тул устгах боломжгүй.", tablename);
            }
            return result;
        }

        #endregion

        #region Committee

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetCommitteeList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committee.*
FROM
    committee
order by updated desc");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetCommittee(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    committee.*
FROM
    committee
where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetCommittee(Committee request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.id == 0)
            {
                cmd.CommandText(@"select coalesce(max(id),0)+1 newid from committee");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.id = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText("select count(1) too from committee where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update committee set 
name=@name,
bossname=@bossname,
phone=@phone,
location=@location,
updated=current_timestamp, 
updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                cmd.AddParam("@bossname", DbType.String, request.bossname, ParameterDirection.Input);
                cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
                cmd.AddParam("@location", DbType.String, request.location, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into committee
(id,
name,
bossname,
phone,
location,
updatedby)
values
(@id,
@name,
@bossname,
@phone,
@location,
@updatedby) 
on duplicate key update 
name=@name,
bossname=@bossname,
phone=@phone,
location=@location,
updated=current_timestamp,
updatedby=@updatedby");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                cmd.AddParam("@bossname", DbType.String, request.bossname, ParameterDirection.Input);
                cmd.AddParam("@phone", DbType.String, request.phone, ParameterDirection.Input);
                cmd.AddParam("@location", DbType.String, request.location, ParameterDirection.Input);
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
        public MResult DeleteCommittee(int id)
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

        #region Project

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetProjectList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    project.*
FROM
    project
order by updated desc");
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
            cmd.CommandText(@"SELECT 
    project.*
FROM
    project
where id = @id");
            cmd.AddParam("@id", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetProject(Project request)
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

            cmd.CommandText("select count(1) too from project where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(@"update project set 
programid =@programid,
name =@name,
funder =@funder,
note =@note,
results =@results,

updated=current_timestamp, 
updatedby=@updatedby 
where id = @id");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@programid", DbType.Int32, request.programid, ParameterDirection.Input);
                cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                cmd.AddParam("@funder", DbType.String, request.funder, ParameterDirection.Input);
                cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
                cmd.AddParam("@results", DbType.String, request.results, ParameterDirection.Input);
                cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
            }
            else
            {
                cmd.CommandText(@"insert into project
(id,
programid,
name,
funder,
note,
results,
updatedby)
values
(@id,
@programid,
@name,
@funder,
@note,
@results,
@updatedby) 
on duplicate key update 
programid=@programid,
name=@name,
funder=@funder,
note=@note,
results=@results,
updated=current_timestamp,
updatedby=@updatedby");
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@programid", DbType.Int32, request.programid, ParameterDirection.Input);
                cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
                cmd.AddParam("@funder", DbType.String, request.funder, ParameterDirection.Input);
                cmd.AddParam("@note", DbType.String, request.note, ParameterDirection.Input);
                cmd.AddParam("@results", DbType.String, request.results, ParameterDirection.Input);
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
        public MResult DeleteProject(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from project where id = @id");
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