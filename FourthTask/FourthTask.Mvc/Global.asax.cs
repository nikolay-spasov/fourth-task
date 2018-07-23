using System.Net.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FourthTask.Mvc.Controllers;
using FourthTask.Mvc.Infrastructure;

namespace FourthTask.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(this.CreateContainer()));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HomeController>();

            builder.RegisterType<HttpClient>()
                .InstancePerRequest();
            builder.RegisterType<ApiClient>()
                .As<IApiClient>()
                .InstancePerRequest();

            return builder.Build();
        }
    }
}
