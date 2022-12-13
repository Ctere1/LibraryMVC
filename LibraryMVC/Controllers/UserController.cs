using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryMVC.HelperMethods;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class UserController : Controller
    {
        LogHelper helper = new LogHelper();
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: User
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: User/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            helper.InsertLog(user.email, "User: " + user.email + " created");
            return View(user);
        }

        // GET: User/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "id,name,email,password")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            helper.InsertLog(user.email, "User: " + user.email + " edited");
            return View(user);
        }

        // GET: User/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            helper.InsertLog(user.email, "User: " + user.email + " deleted");
            return RedirectToAction("Index");
        }

        // GET: User/Books/userId
        [Authorize(Roles = "admin")]
        public ActionResult Books(int? id)
        {
            user user = db.users.Find(id);
            var books = db.books.Where(b => b.borrowedBy == user.email);

            return View(books.ToList());
        }

        // GET: User/UserProfile
        [Authorize(Roles = "user")]
        public ActionResult UserProfile()
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.FirstOrDefault(u => u.email == username);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/UserProfile/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public ActionResult UserProfile([Bind(Include = "id,name,password")] user user)
        {
            user existingUser = db.users.Find(user.id);
            if (ModelState.IsValid)
            {
                existingUser.name = user.name;
                existingUser.password = user.password;
                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "User updated";
                helper.InsertLog(existingUser.email, "User: " + existingUser.email + " updated");
                //return RedirectToAction("UserProfile");
            }
            return View(existingUser);
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
