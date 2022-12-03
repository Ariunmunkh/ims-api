using System;
using Orders.Models;

namespace Orders.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

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
    }
}