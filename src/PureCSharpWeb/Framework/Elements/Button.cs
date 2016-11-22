using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Button: ViewElement
    {

        public Button(): base()
        {

        }

        public Button(object value) : base(value)
        {

        }
        public Button(Expression<Func<string>> expression) : base(expression)
        {
        }

        public Button(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public Button(Expression<Func<int>> expression) : base(expression)
        {
        }

        public Button(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public Button(Expression<Action> expression) : base(expression)
        {
        }
    }
}
