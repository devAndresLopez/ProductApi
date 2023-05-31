using ProductApi.Domain;
using ProductApi.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ProductApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IProductoDomain, ProductoDomain>();
            container.RegisterType<IProductoRepository, ProductoRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}