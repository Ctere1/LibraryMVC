using LibraryMVC.HelperMethods;
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
        LogHelper helper = new LogHelper();
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user, string returnUrl)
        {
            var userInDb = db.users.Any(x => x.email == user.email && x.password == user.password);
            if (userInDb)
            {
                FormsAuthentication.SetAuthCookie(user.email, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //{
                //    return Redirect(returnUrl);
                //}
                helper.InsertLog(user.email, "User logged in");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid user email or password.";
                return View();
            }
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(admin admin)
        {
            var adminInDb = db.admins.Any(x => x.name == admin.name && x.password == admin.password);
            if (adminInDb)
            {
                FormsAuthentication.SetAuthCookie(admin.name, false);
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //{
                //    return Redirect(returnUrl);
                //}
                helper.InsertLog(admin.name, "Admin logged in");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid admin name or password.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            helper.InsertLog(username, "User logged out");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}