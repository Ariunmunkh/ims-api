using BaseLibrary.LConnection;
using Connection.Model;
using LConnection;
using LConnection.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
                request.encryptpass = Infrastructure.Utility.EncryptPass(request.password);

                MCommand cmd = connector.PopCommand();
                cmd.CommandText("select * from tbluser where username = @username");
                cmd.AddParam("@username", DbType.String, request.username, ParameterDirection.Input);
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                if (result.retdata is DataTable data && data.Rows.Count > 0)
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

            int configExpires = 24;
            var now = DateTime.UtcNow;
            var claims = BuildJWTClaims(data);
            var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QXJpdW5tdW5raCBFcmRlbmViaWxlZw=="));

            var jwt = new JwtSecurityToken(
                issuer: "UL",
                audience: "UL",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(configExpires)),
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
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
        private Claim[] BuildJWTClaims(DataTable data)
        {
            string username = string.Empty;
            string email = string.Empty;
            string roleid = string.Empty;
            string volunteerid = string.Empty;
            string committeeid = string.Empty;

            if (data.Rows.Count > 0)
            {
                username = data.Rows[0]["username"].ToString();
                email = data.Rows[0]["email"].ToString();
                roleid = data.Rows[0]["roleid"].ToString();
                volunteerid = data.Rows[0]["volunteerid"].ToString();
                committeeid = data.Rows[0]["committeeid"].ToString();
            }

            var claims = new Claim[]
                {
                    new Claim("username", username),
                    new Claim("email", email),
                    new Claim("roleid", roleid),
                    new Claim("volunteerid", volunteerid),
                    new Claim("committeeid", committeeid),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };
            return claims;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public MResult PasswordRecovery(string username, string email)
        {
            try
            {
                MCommand cmd = connector.PopCommand();
                cmd.CommandText("select * from tbluser where username = @username and email = @email");
                cmd.AddParam("@email", DbType.String, email, ParameterDirection.Input);
                cmd.AddParam("@username", DbType.String, username, ParameterDirection.Input);
                MResult result = connector.Execute(ref cmd, false);
                if (result.rettype != 0)
                    return result;

                if (result.retdata is DataTable data && data.Rows.Count > 0)
                {

                }
                else
                {
                    return new MResult { rettype = -1, retmsg = "Хэрэглэгч бүртгэлгүй байна." };
                }

                return EmailSender.EmailSending(email, "Hi", "test email");
            }
            catch (Exception ex)
            {
                return new MResult { rettype = -1, retmsg = ex.Message };
            }
        }
    }
}