using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductComparisonApi.Utilities
{
    public class EcommerceJsonConfiguration
    {
        public string BaseHost { get; set; }
        public string Host { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string PCode { get; set; }
        public bool? Active { get; set; }
        public string Authority { get; set; }
        public bool? RenderClientSide { get; set; }
        public override string ToString()
        {
            return @$"BaseHost:{BaseHost},Host:{Host},Title:{Title}
                    Description:{Description},Price:{Price}
        ";
        }
    }
}
