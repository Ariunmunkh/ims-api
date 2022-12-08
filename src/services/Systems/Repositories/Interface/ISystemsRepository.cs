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