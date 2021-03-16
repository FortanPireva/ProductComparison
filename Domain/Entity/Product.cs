using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PCode { get; set; }
    }
}
