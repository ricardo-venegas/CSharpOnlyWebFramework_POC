using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Label: ViewElement
    {
        public Label(): base()
        {

        }

        public Label(object value) : base(value)
        {

        }
        public Label(Expression<Func<string>> expression) : base(expression)
        {
        }

        public Label(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public Label(Expression<Func<int>> expression) : base(expression)
        {
        }

        public Label(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public Label(Expression<Action> expression) : base(expression)
        {
        }
    }
}
