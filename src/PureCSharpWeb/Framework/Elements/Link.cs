using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Link: ViewElement
    {
        public Link(): base()
        {

        }

        public Link(Expression<Func<string>> expression): base(expression)
        {

        }

        public Link(Expression<Func<decimal>> expression) : base(expression)
        {

        }
    }
}
