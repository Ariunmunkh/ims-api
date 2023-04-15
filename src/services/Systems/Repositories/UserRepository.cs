using BaseLibrary.LConnection;
using Connection.Model;
using Infrastructure;
using LConnection.Model;
using System;
using System.Data;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public UserRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MResult GetUserList()
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    tbluser.userid,
    tbluser.username,
    tbluser.email,
    tbluser.password,
    tbluser.roleid,
    tbluser.coachid,
    coach.name coachname,
    coach.phone,
    tbluser.districtid,
    district.name districtname,
case when tbluser.roleid = 2 then district.name
     when tbluser.roleid = 3 then coach.name
     else null end rolename,
    date_format(tbluser.updated, '%Y-%m-%d %H:%i:%s') updated
FROM
    tbluser
 LEFT JOIN coach 
   ON coach.coachid = tbluser.coachid
 left join district
   on district.districtid = tbluser.districtid
ORDER BY tbluser.username");
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public MResult GetUser(int userid)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText(@"SELECT 
    tbluser.userid,
    tbluser.username,
    tbluser.email,
    tbluser.roleid,
    tbluser.coachid,
    tbluser.districtid,
    coach.name,
    coach.phone,
    coach.districtid,
    tbluser.updated
FROM
    tbluser
        LEFT JOIN
    coach ON coach.coachid = tbluser.coachid
WHERE
    tbluser.userid = @userid");
            cmd.AddParam("@userid", DbType.Int32, userid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbluser"></param>
        /// <returns></returns>
        public MResult SetUser(tbluser tbluser)
        {
            string updatepasssql = "password = password,";
            tbluser.encryptpass = Utility.EncryptPass("5");
            if (!string.IsNullOrEmpty(tbluser.password))
            {
                tbluser.encryptpass = Utility.EncryptPass(tbluser.password);
                updatepasssql = "password = @password,";
            }

            MCommand cmd = connector.PopCommand();

            if (tbluser.userid == 0)
            {
                cmd.CommandText(@"select coalesce(max(userid),0)+1 newid from tbluser");
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    tbluser.userid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            cmd.CommandText(string.Format(@"INSERT INTO tbluser
  (userid, username, email, password, roleid, coachid, districtid)
values
  (@userid, @username, @email, @password, @roleid, @coachid, @districtid) 
ON DUPLICATE KEY UPDATE 
username = @username, 
roleid = @roleid, 
coachid = @coachid, 
districtid = @districtid, 
email = @email, {0}
updated = current_timestamp", updatepasssql));
            cmd.AddParam("@userid", DbType.Int32, tbluser.userid, ParameterDirection.Input);
            cmd.AddParam("@username", DbType.String, tbluser.username, ParameterDirection.Input);
            cmd.AddParam("@email", DbType.String, tbluser.email, ParameterDirection.Input);
            cmd.AddParam("@password", DbType.String, tbluser.encryptpass, ParameterDirection.Input);
            cmd.AddParam("@roleid", DbType.Int32, tbluser.roleid, ParameterDirection.Input);
            cmd.AddParam("@coachid", DbType.Int32, tbluser.coachid, ParameterDirection.Input);
            cmd.AddParam("@districtid", DbType.Int32, tbluser.districtid, ParameterDirection.Input);
            return connector.Execute(ref cmd, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public MResult DeleteUser(int userid)
        {

            MCommand cmd = connector.PopCommand();
            cmd.CommandText("delete from tbluser where userid = @userid");
            cmd.AddParam("@userid", DbType.Int32, userid, ParameterDirection.Input);
            return connector.Execute(ref cmd, false);

        }
    }
}