using System;

namespace Deliveries.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliveryViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AddressViewModel From { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AddressViewModel To { get; set; }
    }
}