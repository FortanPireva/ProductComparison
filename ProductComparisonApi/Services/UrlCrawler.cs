using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProductComparisonApi.Utilities;
using PuppeteerSharp;
using Product = Domain.Entity.Product;

namespace ProductComparisonApi.Services
{
    public class UrlCrawler
    {
        public Product Crawl(EcommerceJsonConfiguration configuration, string url)
        {
            try
            {
                if (configuration.RenderClientSide != null && (bool)configuration.RenderClientSide)
                {
                    return CrawlClientSide(configuration, url).Result;
                }
                return CrawlHtml(configuration, url);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(configuration.ToString());
                return new Product();
            }
           

        }

        private async Task<Product> CrawlClientSide(EcommerceJsonConfiguration configuration, string url)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            Page page = await browser.NewPageAsync();
            await page.GoToAsync(url);
            var titleNode=await page.WaitForXPathAsync(configuration.Title);
            var descriptionNode = await page.XPathAsync(configuration.Description);
            var priceNode = await page.XPathAsync(configuration.Price);
            var pCodeNode = await page.XPathAsync(configuration.PCode);


            return new Product();
        }

        private Product CrawlHtml(EcommerceJsonConfiguration configuration,string url)
        {
            HtmlWeb web = new HtmlWeb();
           
                var htmlDoc = web.Load(url);
                var node = htmlDoc.DocumentNode.SelectSingleNode("s");
                string Title = CheckIfNullOrDefault(htmlDoc.DocumentNode.SelectSingleNode(configuration.Title));
                string Description = "";
                if (!configuration.Description.Equals(""))
                        Description= CheckIfNullOrDefault(htmlDoc.DocumentNode.SelectSingleNode(configuration.Description));
                string PriceStr = CheckIfNullOrDefault(htmlDoc.DocumentNode.SelectSingleNode(configuration.Price),
                    defaultString: "0.0");
                double Price;
                Regex r = new Regex("(?:[^0-9,.])", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                PriceStr = r.Replace(PriceStr, String.Empty);
                double.TryParse(PriceStr, out Price);
                string PCode = "";
                if (!configuration.PCode.Equals(""))
                {
                    PCode = CheckIfNullOrDefault(htmlDoc.DocumentNode.SelectSingleNode(configuration.PCode));

                }
                var product = new Product()
                {
                    Title = Title,
                    Description = Description,
                    Price = Price,
                    PCode = PCode
                };
                return product;

         
            
        }
        private string  CheckIfNullOrDefault(HtmlNode node,string defaultString="")
        {
            if (node is null)
            {
                return defaultString;
            }
            return node.InnerText;
        }

    }
}
