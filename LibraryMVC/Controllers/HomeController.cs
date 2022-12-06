using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace LibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult MakeBookChartAdmin()
        {
            var activeBooks = db.books.Where(b => b.isActive == true).Count();
            var dueDateExpiredBooks = db.books.Where(b => b.issuedTo < DateTime.Now).Count();
            var allBooks = db.books.Count();
            var model = new[] { allBooks.ToString(), activeBooks.ToString(), dueDateExpiredBooks.ToString() };

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult MakeUserChartAdmin()
        {
            var allUsers = db.users.Count();
            var model = new[] { allUsers.ToString() };

            return View(model);
        }

        [Authorize(Roles = "user")]
        public ActionResult MakeBookChartUser()
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var myBooks = db.books.Where(b => b.borrowedBy == username).Count();
            var dueDateExpiredBooks = db.books.Where(b => b.borrowedBy == username && b.issuedTo < DateTime.Now).Count();

            var model = new[] { myBooks.ToString(), dueDateExpiredBooks.ToString() };

            return View(model);
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