using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    [AllowAnonymous]
    public class SignupController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Signup([Bind(Include = "name,email,password")] user user)
        {
            if (user.email == null || user.password == null || user.name == null)
            {
                ViewBag.Message = "Fill the fields";
                return View();
            }
            var userInDb = db.users.Any(x => x.email == user.email);
            if (!userInDb)
            {
                db.users.Add(user);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "User already exist";
                return View();
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
