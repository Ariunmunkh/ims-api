using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using System.Data;
using HouseHolds.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace HouseHolds.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        private readonly DWConnector connector;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        /// <param name="_httpClientFactory"></param>
        public BaseRepository(DWConnector _connector, IHttpClientFactory _httpClientFactory)
        {
            connector = _connector;
            this._httpClientFactory = _httpClientFactory;
        }

        #region District

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetDistrictList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    districtid,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    district
order by updated desc");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetDistrict(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    districtid,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    district
where districtid = @districtid");
            cmd.AddParam("@districtid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetDistrict(district request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.districtid == 0)
            {
                cmd.CommandText(@"select coalesce(max(districtid),0)+1 newid from district");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.districtid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into district
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

            cmd.AddParam("@districtid", DbType.Int32, request.districtid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
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

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("select count(1) too from household where districtid = @districtid");
            cmd.AddParam("@districtid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн бүртгэлд ашигласан тул устгах боломжгүй." };

            cmd.CommandText("delete from district where districtid = @districtid");
            return connector.Execute(ref cmd, false);

        }

        #endregion


        #region Relationship

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetRelationshipList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
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
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MResult GetRelationship(int id)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    relationshipid,
    name,
    ishead,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    relationship
where relationshipid = @relationshipid");
            cmd.AddParam("@relationshipid", DbType.Int32, id, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult SetRelationship(relationship request)
        {

            MCommand cmd = connector.PopCommand();
            MResult result;

            if (request.relationshipid == 0)
            {
                cmd.CommandText(@"select coalesce(max(relationshipid),0)+1 newid from relationship");
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    request.relationshipid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(@"insert into relationship
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

            cmd.AddParam("@relationshipid", DbType.Int32, request.relationshipid, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
            cmd.AddParam("@ishead", DbType.Boolean, request.ishead, ParameterDirection.Input);
            cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
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

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("select count(1) too from householdmember where relationshipid = @relationshipid");
            cmd.AddParam("@relationshipid", DbType.Int32, id, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable data && data.Rows.Count > 0 && Convert.ToDecimal(data.Rows[0]["too"]) > 0)
                return new MResult { rettype = 1, retmsg = "Өрхийн гишүүн бүртгэлд ашигласан тул устгах боломжгүй." };

            cmd.CommandText("delete from relationship where relationshipid = @relationshipid");
            return connector.Execute(ref cmd, false);

        }

        #endregion

        #region DropDownItem

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetDropDownItemList(string type)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(string.Format(@"SELECT 
    id,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
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
    id,
    name,
    DATE_FORMAT(updated, '%Y-%m-%d %H:%i:%s') updated
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
        public MResult SetDropDownItem(dropdownitem request)
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

            cmd.CommandText(string.Format(@"insert into {0}
(id,
name,
updatedby)
values
(@id,
@name,
@updatedby) 
on duplicate key update 
name=@name,
updated=current_timestamp,
updatedby=@updatedby", request.type));

            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
            cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<MResult> GetSurvey()
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://kobonew.ifrc.org/assets/aB3cZfRrraeAVbRcSm3nNe/submissions/?format=json") { Headers = { { HeaderNames.Authorization, "Token 8a3756d5136bb71b85b89ad790d17d22a5cc09e4" } } };
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();
                    var jsonLinq = JArray.Parse(jsonData);

                    MResult result;
                    MCommand cmd = connector.PopCommand();
                    cmd.CommandText(@"insert into householdsurvey
(householdid,
survey,
updatedby)
values
(@householdid,
@survey,
@updatedby) 
on duplicate key update 
survey=@survey,
updated=current_timestamp,
updatedby=@updatedby");

                    cmd.AddParam("@householdid", DbType.Int32, ParameterDirection.Input);
                    cmd.AddParam("@survey", DbType.String, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                    foreach (var item in jsonLinq)
                    {
                        cmd.SetParamValue("@householdid", Convert.ToInt32(item["group1/id_pull"]));
                        cmd.SetParamValue("@survey", item.ToString());
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;
                    }

                }
                return new MResult { };

            }
            catch (WebException ex)
            {

                return new MResult { retdata = ex };
            }
            catch (Exception ex)
            {

                return new MResult { retdata = ex };
            }
        }

    }
}