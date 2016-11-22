using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TableLayout: TableLayoutBase
    {
        public TableLayout(): base()
        {
            this.Rows = new List<TableLayoutRow>();
        }      
    }
}
