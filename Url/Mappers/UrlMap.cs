using CsvHelper.Configuration;

namespace URL.Mappers
{
    public sealed class UrlMap : ClassMap <Url> 
    {
        public UrlMap(){
            Map(x => x.href).Name("href");
        }
    }
}