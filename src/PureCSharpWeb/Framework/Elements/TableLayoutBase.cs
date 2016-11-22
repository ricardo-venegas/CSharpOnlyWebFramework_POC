using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TableLayoutBase : ViewElement
    {
        List<TableLayoutRow> rows;
        public TableLayoutBase(): base()
        {
            this.Rows = new List<TableLayoutRow>();
        }

        public List<TableLayoutRow> Rows
        {
            get
            {
                return rows;
            }

            set
            {
                rows = value;
            }
        }
    }
}
