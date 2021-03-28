using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entity;

namespace URL.Sevices
{
    public class ApiCaller
    {
        public string url { get; set; }
        public string ProductUri { get; set; }
        public ApiCaller(string url)
        {
            this.url = url;
        }

        public async  Task<Product> Request(string productUri)
        {
            using (HttpClient client = new HttpClient())
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                string json = JsonSerializer.Serialize(new {url = productUri});
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await  client.PostAsync(url,httpContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    Product product = JsonSerializer.Deserialize<Product>(jsonString,options);
                    return product;
                }

                return null;
            }

        }
        
    }
}