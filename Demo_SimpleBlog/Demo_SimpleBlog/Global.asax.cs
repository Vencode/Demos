using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Demo_SimpleBlog.App_Start;
using WebGrease.Configuration;

namespace Demo_SimpleBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.EnsureInitialized(); 
            Database.Configure();
        }

        protected void Application_BeginRequest()
        {
            Database.OpenSession();
        }

        protected void Application_EndRequest()
        {
            Database.CloseSession();
        }
    }
}
