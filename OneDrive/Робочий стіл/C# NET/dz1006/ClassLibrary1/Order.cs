using System;
using System.Collections.Generic;

namespace ShopLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }
}
