using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductComparisonApi.Services;
using ProductComparisonApi.Utilities;

namespace ProductComparisonApi.Extensions
{
    public static class ProjectExtensions
    {
        public static  void AddEcommerceJsonConfiguration(this IServiceCollection services)
        {
            var jsonString =
                File.ReadAllText(
                    @"Utilities\ecommerce-xpaths.json");
            var config =
                 JsonSerializer.Deserialize<List<EcommerceJsonConfiguration>>(jsonString);
            services.AddSingleton(config);
            services.AddSingleton(typeof(UrlFormatter));
            services.AddSingleton(typeof(UrlCrawler));

        }
    }
}
