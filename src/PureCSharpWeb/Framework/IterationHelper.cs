using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework
{
    public static class IterationHelper
    {
        public static T Current<T>(this IEnumerable<T>  t)
        {
            throw new InvalidOperationException("This funciton is for declaration only");
        }
    }
}
