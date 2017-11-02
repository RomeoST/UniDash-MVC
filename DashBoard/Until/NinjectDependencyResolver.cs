using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using Ninject;
using Ninject.Modules;

namespace DashBoard.Until
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            var modules = new INinjectModule[] { new ServiceModule("UniversityContext"), new DashboardModule() };
            kernel = new StandardKernel(modules);
            try
            {
                AddBindings();
            }
            catch (Exception e)
            {
                kernel.Dispose();
            }

        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);


        private void AddBindings()
        {

        }
    }
}