using PureCSharpWeb.Framework;
using PureCSharpWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Controllers
{
    public class OrderController : IController<Order, string>
    {
        public bool Delete(string modelkey)
        {
            throw new NotImplementedException();
        }

        public Order Load(string modelKey)
        {
            throw new NotImplementedException();
        }

        public Order New()
        {
            throw new NotImplementedException();
        }

        public bool Save(Order model)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order model)
        {
            throw new NotImplementedException();
        }

        public static Order AddItem(Order order)
        {
            order.NewLineItem.ExtendedPrice = order.NewLineItem.Price * order.NewLineItem.Quantity;
            order.LineItems.Add(order.NewLineItem.Clone());
            order.NewLineItem.ProductId = "";
            order.NewLineItem.Price = 1.00m;
            order.NewLineItem.Quantity = 1;
            order.NewLineItem.ExtendedPrice = 1.00m;
            return order;
        }
    }
}
