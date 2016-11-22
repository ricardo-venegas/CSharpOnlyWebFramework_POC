using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class EntitySelector: ViewElement
    {
        public EntitySelector(): base()
        {

        }

        public EntitySelector(Expression<Func<string>> expression) : base(expression)
        {
        }

        public EntitySelector(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public EntitySelector(Expression<Func<int>> expression) : base(expression)
        {
        }

        public EntitySelector(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public EntitySelector(Expression<Action> expression) : base(expression)
        {
        }

    }
}
