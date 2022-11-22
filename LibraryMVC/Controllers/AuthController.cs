using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryMVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user, string returnUrl)
        {
            var userInDb = db.users.Any(x => x.name == user.name && x.password == user.password);
            var adminInDb = db.admins.Any(x => x.name == user.name && x.password == user.password);
            if (userInDb || adminInDb)
            {
                FormsAuthentication.SetAuthCookie(user.name, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //{
                //    return Redirect(returnUrl);
                //}
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid user name or password.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}