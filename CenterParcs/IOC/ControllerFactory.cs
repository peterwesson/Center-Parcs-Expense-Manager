using System;
using System.Web.Mvc;
using System.Web.Routing;

using Ninject;

namespace CenterParcs.IOC
{
    public sealed class ControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public ControllerFactory()
        {
            this._kernel = new StandardKernel(new CenterParcsNinjectModule());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return this._kernel.Get(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var dispose = controller as IDisposable;

            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}