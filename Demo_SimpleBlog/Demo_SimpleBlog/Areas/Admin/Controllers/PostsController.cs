using System;
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

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId != null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                return HttpNotFound();

            return View("Form", new PostsForm
            {
                IsNew   = false,
                PostId  = id,
                Content = post.Content,
                Slug    = post.Slug,
                Title   = post.Title 
            });
        }

       [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Thrash(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            Database.Session.Update(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            Database.Session.Delete(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = null;
            Database.Session.Update(post);

            return RedirectToAction("Index");
        }
    }
}