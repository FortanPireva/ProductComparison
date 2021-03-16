using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductComparisonApi.Utilities;

namespace ProductComparisonApi.Services
{
    public class UrlFormatter 
    {
        public List<EcommerceJsonConfiguration> _config { get; set; }

        public UrlFormatter(List<EcommerceJsonConfiguration> _config)
        {
            this._config = _config;
        }

        public EcommerceJsonConfiguration ConfigForUrl(string url)
        {
            if (url.StartsWith("https"))
            {
                url=url.Remove(url.IndexOf("s"),1);
            }

            
            EcommerceJsonConfiguration config = null;
            foreach (var ecommerceJsonConfiguration in _config)
            {
                if (url.StartsWith(ecommerceJsonConfiguration.Host))
                {
                    config = ecommerceJsonConfiguration;
                    return config;
                }
            }

            return config;
        }
    }
}
