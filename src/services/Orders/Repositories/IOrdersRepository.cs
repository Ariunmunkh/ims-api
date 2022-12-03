using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        IEnumerable<Order> GetClientOrders(int clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order GetOrder(int orderId);
    }
}