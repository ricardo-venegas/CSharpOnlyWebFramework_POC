using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class ModelRefreshButtonBase: Button
    {
        public ModelRefreshButtonBase(Expression<Func<string>> expression) : base(expression)
        {
        }

        public ModelRefreshButtonBase(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public ModelRefreshButtonBase(Expression<Func<int>> expression) : base(expression)
        {
        }

        public ModelRefreshButtonBase(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public ModelRefreshButtonBase(Expression<Action> expression) : base(expression)
        {
        }

        public MethodInfo OnClickMethod { get; set; }
    }
}
