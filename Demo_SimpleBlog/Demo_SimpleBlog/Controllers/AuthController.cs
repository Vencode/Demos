using System;
using System.Web.Mvc;
using System.Web.Security;
using Demo_SimpleBlog.ViewModels;

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
            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(form.Username, true);

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("Home");
        }
    }
}