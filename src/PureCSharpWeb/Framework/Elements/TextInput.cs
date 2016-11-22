using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TextInput: ViewElement
    {
        public TextInput(): base()
        {

        }

        public TextInput(Expression<Func<string>> expression) : base(expression)
        {
        }

        public TextInput(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public TextInput(Expression<Func<int>> expression) : base(expression)
        {
        }

        public TextInput(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public TextInput(Expression<Action> expression) : base(expression)
        {
        }
    }
}
