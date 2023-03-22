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
using System.Collections.Generic;
using System.Collections;

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

            cmd.CommandText("select count(1) too from {0} where id = @id");
            cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);

            result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            if (result.retdata is DataTable cdata && cdata.Rows.Count > 0 && Convert.ToDecimal(cdata.Rows[0][0]) > 0)
            {
                cmd.CommandText(string.Format(@"update {0} set name=@name, updated=current_timestamp, updatedby=@updatedby where id = @id", request.type));
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
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
                cmd.ClearParam();
                cmd.AddParam("@id", DbType.Int32, request.id, ParameterDirection.Input);
                cmd.AddParam("@name", DbType.String, request.name, ParameterDirection.Input);
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
dugaar,
h1,
h2,
h3,
h4,
h5,
h6,
h7,
h8,
h9,
h10,
h11,
h12,
h13,
survey,
updatedby)
values
(@householdid,
@dugaar,
@h1,
@h2,
@h3,
@h4,
@h5,
@h6,
@h7,
@h8,
@h9,
@h10,
@h11,
@h12,
@h13,
@survey,
@updatedby) 
on duplicate key update 
survey=@survey,
updated=current_timestamp,
updatedby=@updatedby");

                    cmd.AddParam("@householdid", DbType.Int32, ParameterDirection.Input);
                    cmd.AddParam("@dugaar", DbType.Int32, ParameterDirection.Input);
                    cmd.AddParam("@h1", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h2", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h3", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h4", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h5", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h6", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h7", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h8", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h9", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h10", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h11", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h12", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@h13", DbType.Decimal, ParameterDirection.Input);
                    cmd.AddParam("@survey", DbType.String, ParameterDirection.Input);
                    cmd.AddParam("@updatedby", DbType.Int32, 1, ParameterDirection.Input);

                    MCommand cmdvisit = connector.PopCommand();
                    cmdvisit.CommandText(@"insert into householdvisit
(visitid,
householdid,
visitdate,
memberid,
coachid,
note,
updatedby)
values
((select coalesce(max(b.visitid),0)+1 from householdvisit b),
@householdid,
@visitdate,
(select max(b.memberid) from householdmember b where b.householdid = @householdid),
(select max(b.coachid) from household b where b.householdid = @householdid),
@note,
@updatedby)");

                    cmdvisit.AddParam("@householdid", DbType.Int32, ParameterDirection.Input);
                    cmdvisit.AddParam("@visitdate", DbType.DateTime, ParameterDirection.Input);
                    cmdvisit.AddParam("@note", DbType.String, "kobo", ParameterDirection.Input);
                    cmdvisit.AddParam("@updatedby", DbType.Int32, -1, ParameterDirection.Input);

                    MCommand cmdduplicate = connector.PopCommand();
                    cmdduplicate.CommandText(@"delete from householdvisit where updatedby = -1");
                    result = connector.Execute(ref cmdduplicate, false);
                    if (result.rettype != 0)
                        return result;

                    int householdid;
                    foreach (var item in jsonLinq)
                    {
                        householdid = Convert.ToInt32(item["group1/id_pull"]);
                        cmd.SetParamValue("@householdid", householdid);
                        cmd.SetParamValue("@dugaar", Convert.ToInt32(item["group1/dugaar"]));
                        cmd.SetParamValue("@h1", ToDecimal(item["g16/g14/h1"]));
                        cmd.SetParamValue("@h2", ToDecimal(item["g16/g14/h2"]));
                        cmd.SetParamValue("@h3", ToDecimal(item["g16/g14/h3"]));
                        cmd.SetParamValue("@h4", ToDecimal(item["g16/g14/h4"]));
                        cmd.SetParamValue("@h5", ToDecimal(item["g16/g14/h5"]));
                        cmd.SetParamValue("@h6", ToDecimal(item["g16/g14/h6"]));
                        cmd.SetParamValue("@h7", ToDecimal(item["g16/g14/h7"]));
                        cmd.SetParamValue("@h8", ToDecimal(item["g16/g14/h8"]));
                        cmd.SetParamValue("@h9", ToDecimal(item["g16/g14/h9"]));
                        cmd.SetParamValue("@h10", ToDecimal(item["g16/g14/h10"]));
                        cmd.SetParamValue("@h11", ToDecimal(item["g16/g14/h11"]));
                        cmd.SetParamValue("@h12", ToDecimal(item["g16/g14/h12"]));
                        cmd.SetParamValue("@h13", ToDecimal(item["g16/g14/h13"]));
                        cmd.SetParamValue("@survey", item.ToString());
                        result = connector.Execute(ref cmd, false);
                        if (result.rettype != 0)
                            return result;


                        cmdvisit.SetParamValue("@householdid",householdid);
                        cmdvisit.SetParamValue("@visitdate", Convert.ToDateTime(item["end"]));
                        result = connector.Execute(ref cmdvisit, false);
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

        private decimal ToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetTreeDropdown(bool issurvey)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT
    household.status,
    householdstatus.name statusname,
    COALESCE(household.districtid,-1) districtid,
    COALESCE(district.name,'Хоосон') districtname,
    COALESCE(household.section,-1) section,
    COALESCE(household.householdgroupid,-1) householdgroupid,
    COALESCE(householdgroup.name,'Хоосон') householdgroupname,
    COALESCE(household.coachid,-1) coachid,
    COALESCE(coach.name,'Хоосон') coachname,
    household.householdid,
    household.name
FROM
    household
        LEFT JOIN
    householdstatus ON householdstatus.id = household.status
        LEFT JOIN
    district ON district.districtid = household.districtid
        LEFT JOIN
    householdgroup ON householdgroup.id = household.householdgroupid
        LEFT JOIN
    coach ON coach.coachid = household.coachid" + (issurvey ? " where exists (select null from householdsurvey where householdsurvey.householdid = household.householdid)" : string.Empty));
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            List<object> retdata = new List<object>();

            if (result.retdata is DataTable data)
            {
                DataRow[] districts;
                DataRow[] sections;
                DataRow[] groups;
                DataRow[] coachs;
                DataRow[] households;
                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(data.DefaultView.ToTable("status", true, "status", "statusname"));
                    ds.Tables.Add(data.DefaultView.ToTable("district", true, "status", "districtid", "districtname"));
                    ds.Tables.Add(data.DefaultView.ToTable("section", true, "status", "districtid", "section"));
                    ds.Tables.Add(data.DefaultView.ToTable("group", true, "status", "districtid", "section", "householdgroupid", "householdgroupname"));
                    ds.Tables.Add(data.DefaultView.ToTable("coach", true, "status", "districtid", "section", "householdgroupid", "coachid", "coachname"));
                    ds.Tables.Add(data.DefaultView.ToTable("household", true, "status", "districtid", "section", "householdgroupid", "coachid", "householdid", "name"));

                    foreach (DataRow status in ds.Tables["status"].Rows)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("key", status["status"]);
                        ht.Add("name", status["statusname"]);
                        List<object> districtlist = new List<object>();

                        districts = ds.Tables["district"].Select("status='" + status["status"] + "'");

                        for (int i = districts.Length - 1; i >= 0; i--)
                        {
                            Hashtable dht = new Hashtable();
                            dht.Add("key", districts[i]["districtid"]);
                            dht.Add("name", districts[i]["districtname"]);
                            List<object> sectionlist = new List<object>();

                            sections = ds.Tables["section"].Select("status='" + status["status"] + "' and districtid='" + districts[i]["districtid"] + "'");
                            for (int j = sections.Length - 1; j >= 0; j--)
                            {
                                Hashtable sht = new Hashtable();
                                sht.Add("key", sections[j]["section"]);
                                sht.Add("name", sections[j]["section"]);
                                List<object> grouplist = new List<object>();

                                groups = ds.Tables["group"].Select("status='" + status["status"] + "' and districtid='" + districts[i]["districtid"] + "' and section='" + sections[j]["section"] + "'");
                                for (int k = groups.Length - 1; k >= 0; k--)
                                {
                                    Hashtable ght = new Hashtable();
                                    ght.Add("key", groups[k]["householdgroupid"]);
                                    ght.Add("name", groups[k]["householdgroupname"]);
                                    List<object> coachlist = new List<object>();

                                    coachs = ds.Tables["coach"].Select("status='" + status["status"] + "' and districtid='" + districts[i]["districtid"] + "' and section='" + sections[j]["section"] + "' and householdgroupid='" + groups[k]["householdgroupid"] + "'");
                                    for (int m = coachs.Length - 1; m >= 0; m--)
                                    {
                                        Hashtable cht = new Hashtable();
                                        cht.Add("key", coachs[m]["coachid"]);
                                        cht.Add("name", coachs[m]["coachname"]);

                                        List<object> householdlist = new List<object>();

                                        households = ds.Tables["household"].Select("status='" + status["status"] + "' and districtid='" + districts[i]["districtid"] + "' and section='" + sections[j]["section"] + "' and householdgroupid='" + groups[k]["householdgroupid"] + "' and coachid = '" + coachs[m]["coachid"] + "'");
                                        for (int n = households.Length - 1; n >= 0; n--)
                                        {
                                            Hashtable hht = new Hashtable();
                                            hht.Add("key", households[n]["householdid"]);
                                            hht.Add("name", households[n]["name"]);
                                            ds.Tables["household"].Rows.Remove(households[n]);
                                            householdlist.Add(hht);
                                        }

                                        ds.Tables["coach"].Rows.Remove(coachs[m]);
                                        cht.Add("household", householdlist);
                                        coachlist.Add(cht);
                                    }

                                    ds.Tables["group"].Rows.Remove(groups[k]);
                                    ght.Add("coach", coachlist);
                                    grouplist.Add(ght);
                                }

                                ds.Tables["section"].Rows.Remove(sections[j]);
                                sht.Add("group", grouplist);
                                sectionlist.Add(sht);
                            }

                            ds.Tables["district"].Rows.Remove(districts[i]);
                            dht.Add("section", sectionlist);
                            districtlist.Add(dht);
                        }

                        ht.Add("district", districtlist);
                        retdata.Add(ht);
                    }
                }
            }

            return new MResult { retdata = retdata };
        }

    }
}