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
    public class SystemsRepository : ISystemsRepository
    {
        private readonly DWConnector connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        public SystemsRepository(DWConnector _connector)
        {
            connector = _connector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult GetUserInfo(authbody request)
        {
            try
            {
                request.encryptpass = Utility.EncryptPass(request.password);

                MCommand command = connector.PopCommand();
                command.CommandText("select * from tbluser where username = @username");
                command.AddParam("@username", DbType.String, request.username, ParameterDirection.Input);
                MResult result = connector.Execute(ref command, false);
                if (result.rettype != 0)
                    return result;

                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {
                    if (request.encryptpass.Equals(data.Rows[0]["password"].ToString()))
                    {
                        return new MResult { retmsg = string.Format("Сайн байна уу. {0}", data.Rows[0]["username"]) };
                    }
                }
                return new MResult { rettype = -1, retmsg = "Нэвтрэх нэр эсвэл нууц үг буруу байна." };
            }
            catch (Exception ex)
            {
                return new MResult { rettype = -1, retmsg = ex.Message };
            }

        }

    }
}