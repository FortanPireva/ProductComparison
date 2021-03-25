using System.ComponentModel.DataAnnotations;
using System;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace URL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start CSV File Reading...");
            var _urlService = new UrlService.UrlService();
            string path =@"..\..\..\csvs\";
            var web = new HtmlWeb();
            for (int i = 1; i <= 100; i++)
            {
                string localPath = path + $"{i}.csv";
                var resultData = _urlService.ReadCSVFile(localPath);
                
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