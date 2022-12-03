
namespace Deliveries.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

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
        public Address FromAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Address ToAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OrderId { get; set; }
    }
}