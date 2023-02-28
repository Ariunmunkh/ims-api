using System.Threading.Tasks;
using BaseLibrary.LConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HouseHolds.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class MConnection
    {
        private readonly RequestDelegate requestDeletegate;
        private readonly IConfiguration configuration;

        /// <summary>Initializes a new instance of the <see cref="MConnection"/> class.</summary>
        /// <param name="_requestDelegate">The request delegate.</param>
        /// <param name="_configuration"></param>
        public MConnection(RequestDelegate _requestDelegate, IConfiguration _configuration)
        {
            requestDeletegate = _requestDelegate;
            configuration = _configuration;
        }

        /// <summary>Invokes the specified context.</summary>
        /// <param name="context">The context.</param>
        /// <param name="con">The connection object</param>
        public async Task Invoke(HttpContext context, DWConnector con)
        {
            string connectionString = "Server=db-mysql-sgp1-81348-do-user-13661090-0.b.db.ondigitalocean.com;" +
                "Port=25060;" +
                "Database=defaultdb;" +
                "Uid=doadmin;" +
                "Pwd=AVNS_fzoPdpHhG59vNnK8Nh6;";

            con.ReloadConnectionString(connectionString);
            using (var c = con.Initialize())
            {
                c.Open();
                await requestDeletegate.Invoke(context);
            }

        }


    }
}
