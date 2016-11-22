using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Container: ViewElement
    {

        public List<ViewElement> Elements { get; set; }

        public Container(): base()
        {
            Elements = new List<ViewElement>();

        }

        public Container(Expression<Func<string>> expression) : base(expression)
        {
            Elements = new List<ViewElement>();
        }

        public Container(Expression<Func<decimal>> expression) : base(expression)
        {
            Elements = new List<ViewElement>();
        }

        public Container(Expression<Func<int>> expression) : base(expression)
        {
            Elements = new List<ViewElement>();
        }

        public Container(Expression<Func<bool>> expression) : base(expression)
        {
            Elements = new List<ViewElement>();
        }

        public Container(Expression<Action> expression) : base(expression)
        {
            Elements = new List<ViewElement>();
        }
    }
}
