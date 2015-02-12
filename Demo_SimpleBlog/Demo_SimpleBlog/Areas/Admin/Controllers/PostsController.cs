using System.Web.Mvc;
using Demo_SimpleBlog.Infrastructure;

namespace Demo_SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin"), SelectedTab("posts")]
    public class PostsController : Controller
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View();
        }
    }
}