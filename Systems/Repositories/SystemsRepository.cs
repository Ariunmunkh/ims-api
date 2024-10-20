using BaseLibrary.LConnection;
using Connection.Model;
using LConnection.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemsRepository : ISystemsRepository
    {
        private readonly DWConnector connector;
        private readonly IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connector"></param>
        /// <param name="_configuration"></param>
        public SystemsRepository(DWConnector _connector, IConfiguration _configuration)
        {
            connector = _connector;
            configuration = _configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MResult GetUserInfo(Authbody request)
        {
            try
            {
                request.encryptpass = Models.Utility.EncryptPass(request.password);

                MCommand cmd = connector.PopCommand();
                cmd.CommandText(@"SELECT 
    tbluser.*, committee.name committee
FROM
    tbluser
        LEFT JOIN
    committee ON committee.id = tbluser.committeeid
WHERE
    tbluser.username = @username or tbluser.email = @username");
                cmd.AddParam("@username", DbType.String, request.username, ParameterDirection.Input);
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                using DataTable data = result.retdata as DataTable ?? new();
                if (data.Rows.Count > 0)
                {
                    if (request.encryptpass.Equals(data.Rows[0]["password"].ToString()))
                    {
                        return new MResult { retmsg = string.Format("Сайн байна уу. {0}", data.Rows[0]["username"]), retdata = GenerateJWT(data) };
                    }
                }
                return new MResult { rettype = -1, retmsg = "Нэвтрэх нэр эсвэл нууц үг буруу байна." };
            }
            catch (Exception ex)
            {
                return new MResult { rettype = -1, retmsg = ex.Message };
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private object GenerateJWT(DataTable data)
        {
            if (data == null)
            {
                throw new InvalidCastException(nameof(data));
            }

            _ = int.TryParse(configuration["JWT:TokenValidityInDays"], out int configExpires);
            var now = DateTime.UtcNow;
            var claims = BuildJWTClaims(data);
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? string.Empty));

            var jwt = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(configExpires)),
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256Signature)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //lock (_lockObj)
            //{
            //    userTokenList.Add(encodedJwt);
            //}

            var tokenResponse = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(configExpires).TotalSeconds,
            };

            return tokenResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Claim> BuildJWTClaims(DataTable data)
        {
            string? username = string.Empty;
            string? email = string.Empty;
            string? roleid = string.Empty;
            string? volunteerid = string.Empty;
            string? committeeid = string.Empty;
            string? committee = string.Empty;

            if (data.Rows.Count > 0)
            {
                username = data.Rows[0]["username"].ToString();
                email = data.Rows[0]["email"].ToString();
                roleid = data.Rows[0]["roleid"].ToString();
                volunteerid = data.Rows[0]["volunteerid"].ToString();
                committeeid = data.Rows[0]["committeeid"].ToString();
                committee = data.Rows[0]["committee"].ToString();
            }

            var claims = new List<Claim>
                {
                    new Claim("username", username ?? string.Empty),
                    new Claim("email", email ?? string.Empty),
                    new Claim("roleid", roleid ?? string.Empty),
                    new Claim("volunteerid", volunteerid ?? string.Empty),
                    new Claim("committeeid", committeeid ?? string.Empty),
                    new Claim("committee", committee ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Sub, username ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };
            return claims;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public MResult PasswordRecovery(string email)
        {
            MCommand cmd = connector.PopCommand();
            cmd.CommandText("select * from tbluser where email = @email");
            cmd.AddParam("@email", DbType.String, email, ParameterDirection.Input);
            MResult result = connector.Execute(ref cmd, false);
            if (result.rettype != 0)
                return result;

            using DataTable data = result.retdata as DataTable ?? new DataTable();
            if (data.Rows.Count > 0)
            {
                Guid guid = Guid.NewGuid();
                string pass = guid.ToString().Substring(0, 4);
                string encryptpass = Systems.Models.Utility.EncryptPass(pass);
                cmd.CommandText("update tbluser set password = @password, updated = current_timestamp where userid = @userid");
                cmd.AddParam("@userid", DbType.Int64, data.Rows[0]["userid"], ParameterDirection.Input);
                cmd.AddParam("@password", DbType.String, encryptpass, ParameterDirection.Input);
                result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                return SendEmail(email, "IMS Password", string.Format("Hi\nNew login password for {0} user is {1}", data.Rows[0]["username"], pass)).Result;
            }
            else
            {
                return new MResult { rettype = -1, retmsg = "Хэрэглэгчийн мэдээлэл олдсонгүй." };
            }
        }

        private async Task<MResult> SendEmail(string recepient, string Subject, string Body)
        {
            using var message = new MailMessage();

            message.To.Add(new MailAddress(recepient, recepient));
            message.From = new MailAddress(configuration["email:username"] ?? "ims@redcross.mn", "IMS");
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = false; // change to true if body msg is in html

            using var client = new SmtpClient("smtp.gmail.com");

            client.UseDefaultCredentials = false;
            client.Port = 587;
            client.Credentials = new NetworkCredential(configuration["email:username"], configuration["email:password"]);
            client.EnableSsl = true;

            try
            {
                await client.SendMailAsync(message); // Email sent
            }
            catch (Exception ex)
            {
                return new MResult { rettype = -1, retdata = ex, retmsg = ex.Message };
            }

            return new MResult { };
        }
    }
}