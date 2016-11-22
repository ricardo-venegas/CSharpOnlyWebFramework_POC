using PureCSharpWeb.Framework.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class TableLayoutCell: Container
    {
        public TableLayoutCell ()
        {
            Width = -1;
        }
        public int Width { get; set; }
    }
}
