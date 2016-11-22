using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public abstract class PageBase: Container
    {
        public string Title { get; set; }

        public object ModelObject { get; set; }

        public object ControllerObject { get; set; }
    }
}
