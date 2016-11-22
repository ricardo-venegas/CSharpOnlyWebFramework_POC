using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TableLayoutRowRepeater<T>: RowLayoutRepeaterBase
    {

        public TableLayoutRowRepeater(Expression<Func<IEnumerable<T>>> expression) : base()
        {
            base.Expression = expression;
        }

    }
}
