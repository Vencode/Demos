using System.Web.Mvc;

namespace Demo_SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return Content("ADMIN's Posts");
        }
    }
}