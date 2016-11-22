using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class NumberInput: ViewElement
    {
        public NumberInput(): base()
        {

        }

        public NumberInput(Expression<Func<string>> expression) : base(expression)
        {
        }

        public NumberInput(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public NumberInput(Expression<Func<int>> expression) : base(expression)
        {
        }

        public NumberInput(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public NumberInput(Expression<Action> expression) : base(expression)
        {
        }
    }
}
