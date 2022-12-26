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

    }
}