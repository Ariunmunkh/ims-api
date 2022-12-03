using System;

namespace Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderStates State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Modified { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClientId { get; set; }
    }
}