using System;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.ServiceDiscovery
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceDiscoveryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfig"></param>
        public static void RegisterConsulServices(this IServiceCollection services, ServiceConfig serviceConfig)
        {
            if (serviceConfig == null)
            {
                throw new ArgumentNullException(nameof(serviceConfig));
            }

            var consulClient = CreateConsulClient(serviceConfig);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceConfig"></param>
        /// <returns></returns>
        private static ConsulClient CreateConsulClient(ServiceConfig serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }
    }
}