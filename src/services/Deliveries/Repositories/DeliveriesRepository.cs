using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Deliveries.Models;

namespace Deliveries.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliveriesRepository : IDeliveriesRepository
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IEnumerable<Delivery> GetOrderDeliveries(int orderId)
        {
            var numberOfDeliveries = _random.Next(0, 10);
            var deliveries = new List<Delivery>(numberOfDeliveries);

            for (var i = 0; i < numberOfDeliveries; i++)
            {
                var deliveryId = DateTime.Now.Millisecond;
                var delivery = CreateMockDelivery(orderId, deliveryId);

                deliveries.Add(delivery);
            }

            return deliveries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Delivery GetDelivery(int id)
        {
            var delivery = CreateMockDelivery(DateTime.Now.Millisecond, id);

            return delivery;
        }

        private static Delivery CreateMockDelivery(int orderId, int deliveryId)
        {
            return new Delivery
            {
                Id = deliveryId,
                OrderId = orderId,
                Comment = "Random comment",
                PhoneNumber = "911",
                FromAddress = CreateMockAddress("Lithuania", "Kaunas", "", "", 0.0, 0.0),
                ToAddress = CreateMockAddress("Lithuania", "Vilnius", "", "", 0.0, 0.0)
            };
        }

        private static Address CreateMockAddress(string country, string city, string streetAddress, string zipCode,
            double latitude, double longitude)
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                Country = country,
                City = city,
                StreetAddress = streetAddress,
                ZipCode = zipCode,
                Latitude = latitude,
                Longitude = longitude
            };
        }
    }
}