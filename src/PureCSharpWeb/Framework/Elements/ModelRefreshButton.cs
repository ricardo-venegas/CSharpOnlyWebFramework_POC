using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PureCSharpWeb.Framework.Elements
{
    public class ModelRefreshButton<M>: ModelRefreshButtonBase
    {
        public ModelRefreshButton(Expression<Func<string>> expression) : base(expression)
        {
        }

        public ModelRefreshButton(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public ModelRefreshButton(Expression<Func<int>> expression) : base(expression)
        {
        }

        public ModelRefreshButton(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public ModelRefreshButton(Expression<Action> expression) : base(expression)
        {
        }

        public Func<M, M> OnClick { set {
                base.OnClickMethod = value.GetMethodInfo();
            } }
    }
}
