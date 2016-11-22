using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<LineItem>();
        }
        public string Id { get; set; }
        public Customer Customer { get; set;}
        public List<LineItem> LineItems { get; set; }
        public LineItem NewLineItem { get; set; }
        public decimal SubTotal;
    }
}
