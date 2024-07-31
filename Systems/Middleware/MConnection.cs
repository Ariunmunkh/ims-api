using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLibrary.LConnection;
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
            string connectionString = "Server=134.209.98.47;" +
                "Port=3333;" +
                "Database=mysql;" +
                "Uid=root;" +
                "Pwd=rqzs1jwpe1rqmk1jndo;";

            con.ReloadConnectionString(connectionString);
            using (var c = con.Initialize())
            {
                c.Open();
                await requestDeletegate.Invoke(context);
            }

        }


    }
}
