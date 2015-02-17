using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Demo_SimpleBlog.Infrastructure;
using Demo_SimpleBlog.Models;
using Demo_SimpleBlog.ViewModels;
using NHibernate.Linq;

namespace Demo_SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        private const int PostsPerPage = 10;

        // GET: Posts
        public ActionResult Index(int page = 1)
        {
            var baseQuery =
                Database.Session.Query<Post>()
                    .Where(pos => pos.DeletedAt == null)
                    .OrderByDescending(pos => pos.CreatedAt);

            var totalPostCount = baseQuery.Count();
            var postsId = baseQuery.Skip((page - 1) * PostsPerPage).Take(PostsPerPage).Select(pos => pos.Id).ToArray();
            var posts = baseQuery.Where(pos => postsId.Contains(pos.Id)).FetchMany(pos => pos.Tags).Fetch(pos => pos.User).ToList();

            return View(new PostsIndex
            {
                Posts = new PageData<Post>(posts, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            var parts = SeparateIdAndSlug(idAndSlug);

            if (parts == null)
                return HttpNotFound();

            var tag = Database.Session.Load<Tag>(parts.Item1);

            if (tag == null)
                return HttpNotFound();

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("tag", new { id = parts.Item1, slug = parts.Item2 });

            var totalPostCount = tag.Posts.Count();

            var postIds = tag.Posts
                .OrderByDescending(pos => pos.CreatedAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Where(pos => pos.DeletedAt == null)
                .Select(pos => pos.Id)
                .ToArray();

            var posts = Database.Session.Query<Post>()
                .OrderByDescending(pos => pos.CreatedAt)
                .Where(pos => postIds.Contains(pos.Id))
                .FetchMany(pos => pos.Tags)
                .Fetch(pos => pos.User)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PageData<Post>(posts, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SeparateIdAndSlug(idAndSlug);

            if (parts == null)
                return HttpNotFound();

            var post = Database.Session.Load<Post>(parts.Item1);

            if (post == null || post.IsDeleted)
                return HttpNotFound();

            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new {id = parts.Item1, slug = parts.Item2});

            return View(new PostsShow
            {
                Post = post
            });
        }

        public Tuple<int, string> SeparateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            
            if(!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return Tuple.Create(id, slug);
        }
    }
}