using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Entities
{
    public class Customer
    {
        public Customer()
        {
            Transactions = new List<Transaction>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
