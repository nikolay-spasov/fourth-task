using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;

using FourthTask.Data;
using FourthTask.Repositories;

namespace FourthTask.Api.Configuration
{
    public class DependencyInjectionConfiguration
    {
        public static IDependencyResolver GetDependencyResolver()
        {
            var container = BuildContainer();
            return new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // EF DbContext
            builder.RegisterType<NorthwindEntities>()
                .InstancePerRequest();

            // Repositories
            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerRequest();
            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerRequest();

            // Controllers
            builder.RegisterApiControllers(typeof(DependencyInjectionConfiguration).Assembly);

            return builder.Build();
        }
    }
}