using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;

using FourthTask.Api.Factories;
using FourthTask.Data;
using FourthTask.Repositories;
using FourthTask.Repositories.Factories;

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

            // Mappers
            builder.RegisterType<DbOrderToDomainOrderMapper>()
                .As<IDbOrderToDomainOrderMapper>()
                .InstancePerRequest();
            builder.RegisterType<DbCustomerToDomainCustomerMapper>()
               .As<IDbCustomerToDomainCustomerMapper>()
               .InstancePerRequest();

            builder.RegisterType<CustomerListRowToCustomerListVMMapper>()
                .As<ICustomerListRowToCustomerListVMMapper>()
                .InstancePerRequest();
            builder.RegisterType<CustomerToCustomerVMMapper>()
                .As<ICustomerToCustomerVMMapper>()
                .InstancePerRequest();
            builder.RegisterType<OrderToOrderVMMapper>()
                .As<IOrderToOrderVMMapper>()
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