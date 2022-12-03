using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Infrastructure.ESwagger
{
    /// <summary>Swagger Extention</summary>
    public static class ESwagger
    {
        /// <summary>Configures the swagger.</summary>
        /// <param name="service">The service.</param>
        /// <param name="title"></param>
        /// <param name="version">  Swagger-н хувилбар (Жишээ нь : v1)</param>
        /// <returns>IServiceCollection</returns>
        /// <exception cref="ArgumentNullException"> Service дамжуулаагүй тохиолдолд</exception>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection service, string title, string version)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (string.IsNullOrEmpty(version))
            {
                version = "v1";
            }

            service.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc(version, new Info { Title = title, Version = version });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, title + ".xml ");
                conf.IncludeXmlComments(xmlPath);
            });

            return service;
        }
    }
}