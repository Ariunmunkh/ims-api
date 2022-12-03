using System;
using System.Collections;
using System.Collections.Generic;
using Deliveries.Models;

namespace Deliveries.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeliveriesRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IEnumerable<Delivery> GetOrderDeliveries(int orderId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Delivery GetDelivery(int id);
    }
}