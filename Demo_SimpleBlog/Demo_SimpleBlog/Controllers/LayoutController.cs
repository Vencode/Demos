using System.Linq;
using System.Web.Mvc;
using Demo_SimpleBlog.Models;
using Demo_SimpleBlog.ViewModels;
using NHibernate.Linq;

namespace Demo_SimpleBlog.Controllers
{
    public class LayoutController : Controller
    {
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return View(new LayoutSideBar
            {
                IsLoggedIn = Auth.User != null,
                Username = Auth.User != null ? Auth.User.Username : "",
                IsAdmin = User.IsInRole("Admin"),
                Tags = Database.Session.Query<Tag>().Select(tag => new
                {
                    tag.Id,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                }).Where(tg => tg.PostCount > 0)
                .OrderByDescending(p => p.PostCount)
                .Select(tag => new SidebarTag(tag.Id, tag.Name, tag.Slug, tag.PostCount))
                .ToList()
            });
        }
    }
}