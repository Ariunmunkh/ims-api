using Connection.Model;
using Systems.Models;

namespace Systems.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MResult GetUserList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        MResult GetUser(int userid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbluser"></param>
        /// <returns></returns>
        MResult SetUser(Tbluser tbluser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        MResult DeleteUser(int userid);
    }
}