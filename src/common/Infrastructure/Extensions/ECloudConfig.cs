using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Extensions.Configuration;

using Winton.Extensions.Configuration.Consul;

namespace Infrastructure.ECloudConfig
{
    /// <summary></summary>
    public static class ECloudConfig
    {
        private static readonly string DEFAULT_PREFIX_KEY = "initial.json";

        /// <summary>Registers the dw configuration.</summary>
        /// <param name="builder">The builder.</param>
        /// <param name="address"></param>
        /// <param name="keyPrefixes">The key prefixes.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">builder</exception>
        public static IConfigurationBuilder RegisterDWConfig(this IConfigurationBuilder builder, string address, List<string> keyPrefixes)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (keyPrefixes == null || keyPrefixes.Count <= 0)
            {
                keyPrefixes.Add(DEFAULT_PREFIX_KEY);
            }

            // Needs more efficient way to skip options
            var tokenSource = new CancellationTokenSource();
            foreach (string keyPrefix in keyPrefixes)
            {
                builder.AddConsul(
                keyPrefix, tokenSource.Token, options =>
                {
                    options.ConsulConfigurationOptions =
                        consul => { consul.Address = new Uri(address); };
                    options.Optional = true;
                    options.ReloadOnChange = true;
                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                });
            }

            return builder;
        }
    }
}