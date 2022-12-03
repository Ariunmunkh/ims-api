using System;

namespace Infrastructure.ServiceDiscovery
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public Uri ServiceDiscoveryAddress { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Uri ServiceAddress { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ServiceId { get; set; }
    }
}
