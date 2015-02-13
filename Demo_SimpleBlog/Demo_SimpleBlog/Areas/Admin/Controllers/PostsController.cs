using System.Linq;
using System.Web.Mvc;
using Demo_SimpleBlog.Areas.Admin.ViewModels;
using Demo_SimpleBlog.Infrastructure;
using Demo_SimpleBlog.Models;
using NHibernate.Linq;

namespace Demo_SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin"), SelectedTab("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;

        // GET: Admin/Posts
        public ActionResult Index(int page = 1)
        {
            var totalPostCount = Database.Session.Query<Post>().Count();

            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(pos => pos.CreatedAt)
                .Skip((page - 1)*PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PageData<Post>(currentPostPage, totalPostCount, page, PostsPerPage)
            });
        }
    }
}