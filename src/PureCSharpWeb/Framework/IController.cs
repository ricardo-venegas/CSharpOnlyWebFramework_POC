using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework
{
    public interface IController<T,K>
    {

        T New();

        T Load(K modelKey);

        T Update(T model);

        bool Save(T model);

        bool Delete(K modelkey);
    }
}
