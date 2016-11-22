using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework.Elements
{
    public class Page<M,C,K> : PageBase where C:IController <M,K>
    {
        private M _model;

        private C _controller;

        public Page(M model, C controller)
        {
            this.Model = model;
            this.Controller = controller;
        }

        public M Model
        {
            get
            {
                return _model;
            }

            set
            {
                ModelObject = value;
                _model = value;
            }
        }

        public C Controller
        {
            get
            {
                return _controller;
            }

            set
            {
                ControllerObject = value;
                _controller = value;
            }
        }
    }
}
