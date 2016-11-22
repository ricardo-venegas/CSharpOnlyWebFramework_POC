using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class ViewElement
    {
        //Expression<Func<string>> StringExpression { get; set; }
        public LambdaExpression Expression { get; set; }
        public object Value { get; set; }

        public ViewElement()
        {

        }

        public ViewElement(LambdaExpression expression)
        {
            this.Expression = expression;
            Value = null;
        }


        public ViewElement(Object value)
        {
            this.Expression = null;
            Value = value;
        }
    }
}
