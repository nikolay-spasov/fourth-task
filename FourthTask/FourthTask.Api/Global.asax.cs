using System.Web.Http;

using FourthTask.Api.Configuration;

namespace FourthTask.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = DependencyInjectionConfiguration.GetDependencyResolver();
        }
    }
}
