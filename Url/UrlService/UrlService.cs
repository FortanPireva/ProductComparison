using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using URL.Mappers;
using CsvHelper.Configuration;
using System.Globalization;

namespace URL.UrlService
{
    public class UrlService
    {
        public List <Url> ReadCSVFile(string location) {
            try {
                using(var reader = new StreamReader(location, Encoding.Default))
                using(var csv = new CsvReader(reader,CultureInfo.InvariantCulture)) {
                    csv.Context.RegisterClassMap<UrlMap>();
                    var records = csv.GetRecords<Url>().ToList();
                    Console.WriteLine("Read from file "+location);
                    return records;
                }
            } catch (Exception e)
            {
                var file = File.Create(location);
                using(var reader = new StreamReader(file, Encoding.Default))
                using(var csv = new CsvReader(reader,CultureInfo.InvariantCulture)) {
                    csv.Context.RegisterClassMap<UrlMap>();
                    var records = csv.GetRecords<Url>().ToList();
                    Console.WriteLine("Created file "+location);

                    Console.WriteLine("Read from file "+location);

                    return records;
                }
            }
        }
        public void WriteCSVFile(string path, List <Url> Links) {
            using(StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using(CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture)) {
                cw.WriteHeader<Url>();
                cw.NextRecord();
                foreach(Url url in Links) {
                    cw.WriteRecord(url);
                    cw.NextRecord();
                }
                
                Console.WriteLine("Writed to file "+path);

            }
        }
    }
}