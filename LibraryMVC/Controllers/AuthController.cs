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

        // GET: Auht
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user)
        {
            var userInDb = db.users.FirstOrDefault(x => x.name == user.name && x.password == user.password);
            var adminInDb = db.admins.FirstOrDefault(x => x.name == user.name && x.password == user.password);
            if (userInDb != null)
            {
                //Roles.CreateRole("user");
                //if (!Roles.IsUserInRole(user.name, "user"))
                //    Roles.AddUserToRole(user.name, "user");
                FormsAuthentication.SetAuthCookie(user.name, false);
                return RedirectToAction("Index", "Home");
            }
            else if (adminInDb != null)
            {
                //Roles.CreateRole("admin");
                //if (!Roles.IsUserInRole(user.name, "admin"))
                //    Roles.AddUserToRole(user.name, "admin");
                FormsAuthentication.SetAuthCookie(user.name, false);
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