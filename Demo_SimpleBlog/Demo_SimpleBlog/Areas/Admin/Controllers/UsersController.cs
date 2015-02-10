using System.Linq;
using System.Web.Mvc;
using Demo_SimpleBlog.Areas.Admin.ViewModels;
using Demo_SimpleBlog.Infrastructure;
using Demo_SimpleBlog.Models;
using NHibernate.Linq;

namespace Demo_SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }
    }
}