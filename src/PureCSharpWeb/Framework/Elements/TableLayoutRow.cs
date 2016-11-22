using PureCSharpWeb.Framework.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TableLayoutRow: ViewElement
    {
        List<TableLayoutCell> cells;
        public TableLayoutRow(): base()
        {
            this.Cells= new List<TableLayoutCell>();
        }

        public TableLayoutRow(Expression<Func<string>> expression) : base(expression)
        {
        }

        public TableLayoutRow(Expression<Func<decimal>> expression) : base(expression)
        {
        }

        public TableLayoutRow(Expression<Func<int>> expression) : base(expression)
        {
        }

        public TableLayoutRow(Expression<Func<bool>> expression) : base(expression)
        {
        }

        public TableLayoutRow(Expression<Action> expression) : base(expression)
        {
        }

        public List<TableLayoutCell> Cells
        {
            get
            {
                return cells;
            }

            set
            {
                cells = value;
            }
        }

    }
}
