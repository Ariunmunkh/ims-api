using Connection.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        MResult SetUser(tbluser tbluser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        MResult DeleteUser(int userid);
    }
}