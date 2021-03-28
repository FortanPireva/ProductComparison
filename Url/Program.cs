using System.ComponentModel.DataAnnotations;
using System;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Domain.Entity;
using URL.Mappers;
using URL.Sevices;


namespace URL
{
    class Program
    {
        static void Main(string[] args)
        {
           ProductsToCSV();
        }

        static void ProductsToCSV()
        {
            Console.WriteLine("Start CSV File Reading...");
            var _urlService = new UrlService.UrlService();
            string path =@"..\..\..\csvs\";
            string productsPath =@"..\..\..\productcsvs\";

            string[] csvFiles = Directory.GetFiles(path);
            var web = new HtmlWeb();
            ApiCaller caller = new ApiCaller("http://localhost:5000/productcrawler");
;            foreach (var csvFile in csvFiles)
            {
                string fileName = Path.GetFileName(csvFile);
                var resultData = _urlService.ReadCSVFile<Url,UrlMap>(csvFile);
                var listProducts = _urlService.ReadCSVFile<Product, ProductMap>(productsPath+fileName);
                if (listProducts.Count>0)
                {
                    continue;
                }
                if (resultData.Count==0)
                {
                    continue;
                }
                List<Product> products = new List<Product>();
                foreach (var link in resultData)
                {
                    Product product = caller.Request(link.href).Result;
                    if (product != null)
                    {   
                        products.Add(product);
                        Console.WriteLine("Adding " +product.Title);
                    }
                }
                _urlService.WriteCSVFile(productsPath+fileName, products);
            }
               

            
        }
        static void LinksToCSV()
        {
            Console.WriteLine("Start CSV File Reading...");
            var _urlService = new UrlService.UrlService();
            string path =@"..\..\..\csvs\";
            var web = new HtmlWeb();
            for (int i = 1; i <= 100; i++)
            {
                string localPath = path + $"{i}.csv";
                var resultData = _urlService.ReadCSVFile<Url,UrlMap>(localPath);
                
                var doc = web.Load($"https://gjirafa50.com/index.php?dispatch=categories.view&category_id={i}&isAjax=1&page=0&limit=100");
                var linkTags = doc.DocumentNode.Descendants("link");
                var div = doc.DocumentNode.SelectNodes("//div[@class='ty-grid-list__image']");
                var linkedPages = div.Descendants("a")
                    .Select(a => a.GetAttributeValue("href", null))
                    .Where(u => !String.IsNullOrEmpty(u))
                    .ToList();
                foreach (var l in linkedPages)
                {
                    Url url = new Url();
                    url.href = l;
                    resultData.Add(url);
                }
                _urlService.WriteCSVFile(localPath, resultData);

            }

        }
    }
}