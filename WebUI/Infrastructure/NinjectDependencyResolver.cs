using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete.Repositories;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<ILotRepository>().To<EFLotRepository>();

            _kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();

            _kernel.Bind<IUserRepository>().To<EFUserRepository>();

            _kernel.Bind<IUserRoleRepository>().To<EFUserRoleRepository>();
        }
    }
}