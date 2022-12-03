using System;
using System.Threading.Tasks;
using Deliveries.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Deliveries.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesRepository _deliveriesRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveriesRepository"></param>
        public DeliveriesController(IDeliveriesRepository deliveriesRepository)
        {
            _deliveriesRepository = deliveriesRepository ?? throw new ArgumentNullException(nameof(deliveriesRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("api/deliveries/list")]
        public IActionResult GetOrderDeliveries(int orderId)
        {
            if (orderId == 0)
            {
                throw new ArgumentException(nameof(orderId));
            }
            var deliveries = _deliveriesRepository.GetOrderDeliveries(orderId);

            return Ok(deliveries);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        [HttpGet("api/deliveries/get")]
        public IActionResult GetById(int deliveryId)
        {
            if (deliveryId == 0)
            {
                throw new ArgumentException(nameof(deliveryId));
            }

            var delivery = _deliveriesRepository.GetDelivery(deliveryId);

            return Ok(delivery);
        }
    }
}
