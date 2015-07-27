using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.IoC
{
    /// <summary>
    /// IDependencyResolver implemenation
    /// Implementations of the IDependencyResolver interface should delegate to the underlying dependency injection container to provide the registered service for the requested type. 
    /// When there are no registered services of the requested type, the ASP.NET MVC framework expects implementations of this interface to return null from GetService and to return an empty collection from GetServices.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}
