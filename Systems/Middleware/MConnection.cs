using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BaseLibrary.LConnection;
using Connection.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Systems.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class MConnection
    {
        private readonly RequestDelegate requestDeletegate;

        /// <summary>Initializes a new instance of the <see cref="MConnection"/> class.</summary>
        /// <param name="_requestDelegate">The request delegate.</param>
        public MConnection(RequestDelegate _requestDelegate)
        {
            requestDeletegate = _requestDelegate;
        }

        /// <summary>Invokes the specified context.</summary>
        /// <param name="context">The context.</param>
        /// <param name="con">The connection object</param>
        public async Task Invoke(HttpContext context, DWConnector con)
        {
            con.RequestHeaderInfo = GetInfo(context.Request.Headers);

            string connectionString = "Server=167.172.94.246;" +
                "Port=3333;" +
                "Database=mysql;" +
                "Uid=root;" +
                "Pwd=rqzs1jwpe1rqmk1jndo;";

            con.ReloadConnectionString(connectionString);
            using var c = con.Initialize();
            c.Open();
            await requestDeletegate.Invoke(context);

        }

        /// <summary>
        /// Access token-с хэрэглэгчийн мэдээлэл авах
        /// </summary>
        /// <param name="Headers"></param>
        /// <returns></returns>
        private static RequestInfo GetInfo(IHeaderDictionary Headers)
        {
            try
            {
                RequestInfo headerInfo = new();
                if (Headers.ContainsKey("AUTHORIZATION"))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var claims = handler.ReadJwtToken(Headers["AUTHORIZATION"].ToString().Split(" ")[1].Trim()).Claims;
                    foreach (var claim in claims)
                    {
                        if (claim.Type.Equals("USERID", StringComparison.CurrentCultureIgnoreCase) && int.TryParse(claim.Value, out int userid))
                            headerInfo.UserID = userid;
                        if (claim.Type.Equals("ROLEID", StringComparison.CurrentCultureIgnoreCase) && int.TryParse(claim.Value, out int roleid))
                            headerInfo.Roleid = roleid;
                    }
                }
                return headerInfo;
            }
            catch (Exception)
            {
                return new();
            }
        }

    }
}
