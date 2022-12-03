using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetClientOrders(int clientId)
        {
            var numberOfOrders = _random.Next(0, 4);
            var orders = new List<Order>(numberOfOrders);

            for (var i = 0; i < numberOfOrders; i++)
            {
                var orderId = DateTime.Now.Millisecond;
                var order = CreateMockOrder(clientId, orderId);

                orders.Add(order);
            }

            return orders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrder(int orderId)
        {
            var clientId = DateTime.Now.Millisecond;

            return CreateMockOrder(clientId, orderId);
        }

        private static Order CreateMockOrder(int clientId, int orderId)
        {
            var createdAt = DateTimeOffset.UtcNow;

            return new Order
            {
                Id = orderId,
                ClientId = clientId,
                Created = createdAt,
                Modified = createdAt,
                State = OrderStates.Created
            };
        }
    }
}