using System;

namespace Deliveries.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Address
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Longitude { get; set; }
    }
}