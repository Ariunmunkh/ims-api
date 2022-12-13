using BaseLibrary.LConnection;
using Connection.Model;
using Infrastructure;
using LConnection.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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

            MCommand command = connector.PopCommand();
            command.CommandText("select userid, username, email, password, roleid, updated from tbluser order by username");
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public MResult GetUser(int userid)
        {

            MCommand command = connector.PopCommand();
            command.CommandText("select userid, username, email, roleid, updated from tbluser where userid = @userid");
            command.AddParam("@userid", DbType.Int32, userid, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbluser"></param>
        /// <returns></returns>
        public MResult SetUser(tbluser tbluser)
        {
            string updatepasssql = string.Empty;
            if (!string.IsNullOrEmpty(tbluser.password))
            {
                tbluser.encryptpass = Utility.EncryptPass(tbluser.password);
                updatepasssql = "password = @password,";
            }

            MCommand command = connector.PopCommand();

            if (tbluser.userid == 0)
            {
                command.CommandText(@"select coalesce(max(userid),0)+1 newid from tbluser");
                MResult result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;
                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    tbluser.userid = Convert.ToInt32(data.Rows[0]["newid"]);
                }
            }

            command.CommandText(string.Format(@"INSERT INTO tbluser
  (userid, username, email, password, roleid)
values
  (@userid, @username, @email, @password, @roleid) 
ON DUPLICATE KEY UPDATE 
username = @username, 
roleid = @roleid, 
email = @email, {0}
updated = current_timestamp", updatepasssql));
            command.AddParam("@userid", DbType.Int32, tbluser.userid, ParameterDirection.Input);
            command.AddParam("@username", DbType.String, tbluser.username, ParameterDirection.Input);
            command.AddParam("@email", DbType.String, tbluser.email, ParameterDirection.Input);
            command.AddParam("@password", DbType.String, tbluser.encryptpass, ParameterDirection.Input);
            command.AddParam("@roleid", DbType.String, tbluser.roleid, ParameterDirection.Input);
            return connector.Execute(ref command, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public MResult DeleteUser(int userid)
        {

            MCommand command = connector.PopCommand();
            command.CommandText("delete from tbluser where userid = @userid");
            command.AddParam("@userid", DbType.Int32, userid, ParameterDirection.Input);
            return connector.Execute(ref command, false);

        }
    }
}