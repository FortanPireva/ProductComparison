using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductComparisonApi.Services;

namespace ProductComparisonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCrawlerController : ControllerBase
    {
        private readonly UrlCrawler _urlCrawler;
        private readonly UrlFormatter _urlFormatter;

        public ProductCrawlerController(UrlCrawler urlCrawler, UrlFormatter urlFormatter)
        {
            _urlCrawler = urlCrawler;
            _urlFormatter = urlFormatter;   
        }
        [HttpPost("")]
        public IActionResult GetProduct([FromBody] Url url)
        {

            var config=_urlFormatter.ConfigForUrl(url.url);
            if (config is null)
            {
                return NotFound(new { message="Base Host not found"});
            }
            var product = _urlCrawler.Crawl(config, url.url);
            return Ok(product);
        }

    }

    public class Url
    {
        public string url;
    }
}
