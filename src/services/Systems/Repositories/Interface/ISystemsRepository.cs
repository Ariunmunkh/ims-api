using Connection.Model;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authBody"></param>
        /// <returns></returns>
        MResult GetUserInfo(authbody authBody);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        MResult PasswordRecovery(string username, string email);
    }
}