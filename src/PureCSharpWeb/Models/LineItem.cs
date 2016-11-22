using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Models
{
    public class LineItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ExtendedPrice { get; set; }
        public LineItem Clone()
        {
            return (LineItem)this.MemberwiseClone();
        }
    }
}
