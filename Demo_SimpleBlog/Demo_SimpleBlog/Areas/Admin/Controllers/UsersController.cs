using System.Web.Mvc;
using Demo_SimpleBlog.Infrastructure;

namespace Demo_SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}