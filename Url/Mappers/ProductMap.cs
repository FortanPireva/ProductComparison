using CsvHelper.Configuration;
using Domain.Entity;

namespace URL.Mappers
{
    public sealed class ProductMap :ClassMap<Product>
    {
        ProductMap()
        {
            Map(product => product.Title).Name("Title");
            Map(product => product.Description).Name("Description");
            Map(product => product.Price).Name("Price");
            Map(product => product.PCode).Name("PCode");
        }
    }
}