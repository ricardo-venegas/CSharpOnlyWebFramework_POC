using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Repeater<T>: Container
    {
        public Expression<Func<IEnumerable<T>>> EnumerationExpression { get; set; }

        public Repeater(): base()
        {

        }

        public Repeater(Expression<Func<IEnumerable<T>>> expression) : base()
        {
            EnumerationExpression = expression;
        }

    }
}
