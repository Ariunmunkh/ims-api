using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.ServiceDiscovery
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly ServiceConfig _config;
        private string _registrationId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="config"></param>
        public ServiceDiscoveryHostedService(IConsulClient client, ServiceConfig config)
        {
            _client = client;
            _config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_config.ServiceName}-{_config.ServiceId}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _config.ServiceName,
                Address = _config.ServiceAddress.Host,
                Port = _config.ServiceAddress.Port
            };

            await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _client.Agent.ServiceRegister(registration, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}