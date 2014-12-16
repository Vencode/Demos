using System.Web.Mvc;

namespace Demo_SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            return Content("Hello World");
        }
    }
}