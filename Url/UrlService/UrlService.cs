using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using URL.Mappers;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.Json;
using Domain.Entity;

namespace URL.UrlService
{
    public class UrlService
    {
        public List <T> ReadCSVFile<T,T1>(string location) where  T1 : ClassMap<T>{
            try {
                using(var reader = new StreamReader(location, Encoding.Default))
                using(var csv = new CsvReader(reader,CultureInfo.InvariantCulture)) {
                    csv.Context.RegisterClassMap<T1>();
                    var records = csv.GetRecords<T>().ToList();
                    Console.WriteLine("Read from file "+location);
                    return records;
                }
            } catch (Exception e)
            {
                var file = File.Create(location);
                using(var reader = new StreamReader(file, Encoding.Default))
                using(var csv = new CsvReader(reader,CultureInfo.InvariantCulture)) {
                    csv.Context.RegisterClassMap<ClassMap<T>>();
                    var records = csv.GetRecords<T>().ToList();
                    Console.WriteLine("Created file "+location);

                    Console.WriteLine("Read from file "+location);

                    return records;
                }
            }
        }
        public void WriteCSVFile<T>(string path, List<T> objects)
        {
            FileStream file = null;
            if (!File.Exists(path))
            {
                file =File.Create(path);
            }
            else
            {

                file = File.Open(path,FileMode.Create);
            }
            
            using(StreamWriter sw = new StreamWriter(file))
            using(CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture)) {
                cw.WriteHeader<T>();
                cw.NextRecord();
                foreach(T obj in objects) {
                    cw.WriteRecord(obj);
                    cw.NextRecord();
                }
                
                Console.WriteLine("Writed to file "+path);

            }
        }

        public void WriteJsonFile(string path, List<Product> products)
        {
            using (StreamWriter writer=File.CreateText(path))
            {
                var json = JsonSerializer.Serialize(products);
                
                writer.Write(json);
                Console.WriteLine("Wrote to file..."+path);
            }
        }
    }
}