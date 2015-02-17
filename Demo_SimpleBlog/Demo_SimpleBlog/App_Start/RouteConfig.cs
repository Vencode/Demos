using System.Web.Mvc;
using System.Web.Routing;
using Demo_SimpleBlog.Controllers;

namespace Demo_SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] {typeof (PostsController).Namespace};

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Tag", "tag/{id}-{slug}", new { controller = "Posts", action = "Tag" }, namespaces);
            routes.MapRoute("TagForReal", "tag/{idAndSlug}", new { controller = "Posts", action = "Tag" }, namespaces);

            routes.MapRoute("Post", "post/{id}-{slug}", new {controller = "Posts", action = "Show"}, namespaces);
            routes.MapRoute("PostForReal", "post/{idAndSlug}", new {controller = "Posts", action = "Show"}, namespaces);

            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);

            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);

            routes.MapRoute("Home","", new { controller = "Posts", action = "Index"}, namespaces);
            
        }
    }
}
