using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Book
        [Authorize(Roles = "admin,user")]
        public ActionResult Index()
        {
            return View(db.books.ToList());
        }

        // GET: Book/Details/5
        [Authorize(Roles = "admin,user")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "id,name,genre,language,publisherName,authorName,description,isActive,issuedFrom,issuedTo")] book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "id,name,genre,language,publisherName,authorName,description,isActive,issuedFrom,issuedTo,borrowedBy")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Book/MyBooks/5
        [Authorize(Roles = "user")]
        public ActionResult MyBooks()
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var books = db.books.Where(b => b.borrowedBy == username);

            return View(books.ToList());
        }

        // GET: Book/Borrow/5
        [Authorize(Roles = "admin,user")]
        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Borrow/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public ActionResult Borrow([Bind(Include = "id,issuedFrom,issuedTo")] book book)
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            user user = db.users.FirstOrDefault(u => u.email == username);

            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = false;
                existingBook.issuedFrom = book.issuedFrom;
                existingBook.issuedTo = book.issuedTo;
                existingBook.borrowedBy = user.email;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(existingBook);
        }

        // GET: Book/Return/5
        [Authorize(Roles = "admin,user")]
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,user")]
        public ActionResult Return([Bind(Include = "id")] book book)
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            user user = db.users.FirstOrDefault(u => u.email == username);

            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = true;
                existingBook.issuedFrom = null;
                existingBook.issuedTo = null;
                existingBook.borrowedBy = null;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(existingBook);
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
