using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Demo_SimpleBlog.Models;
using Demo_SimpleBlog.ViewModels;
using NHibernate.Linq;

namespace Demo_SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(usr => usr.Username == form.Username);

            if(user == null)
                Models.User.FakeHash();

            if(user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect!");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("Home");
        }
    }
}