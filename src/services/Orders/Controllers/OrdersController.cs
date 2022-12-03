using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Repositories;

namespace Orders.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordersRepository"></param>
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetOrders(int clientId)
        {
            if (clientId == 0)
            {
                throw new ArgumentException(nameof(clientId));
            }

            var orders = _ordersRepository.GetClientOrders(clientId);

            return Ok(orders);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult Get(int orderId)
        {
            if (orderId == 0)
            {
                throw new ArgumentException(nameof(orderId));
            }

            var order = _ordersRepository.GetOrder(orderId);

            return Ok(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_files")]
        public IActionResult GetFileNames()
        {
            string path = Directory.GetCurrentDirectory();
            string[] fileEntries = Directory.GetFiles(path);


            return Ok(fileEntries);
        }
    }
}
