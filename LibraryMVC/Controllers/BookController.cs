﻿using System;
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
using LibraryMVC.HelperMethods;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        LogHelper helper = new LogHelper();
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Book
        [Authorize(Roles = "admin,user")]
        public ActionResult Index(string searchString)
        {
            if (searchString == null)
            {
                return View(db.books.ToList());
            }
            else
            {
                var books = db.books.Where(b => b.name.Contains(searchString) || searchString == null);
                return View(books);
            }
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
        public ActionResult MyBooks(string searchString)
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            if (searchString == null)
            {
                var books = db.books.Where(b => b.borrowedBy == username);
                return View(books.ToList());
            }
            else
            {
                var books = db.books.Where(b => b.name.Contains(searchString) && b.borrowedBy == username);
                return View(books);
            }
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
        public ActionResult Borrow([Bind(Include = "id,issuedTo")] book book)
        {
            string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            user user = db.users.FirstOrDefault(u => u.email == username);

            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = false;
                existingBook.issuedFrom = DateTime.Now;
                existingBook.issuedTo = book.issuedTo;
                if (book.issuedTo == null || book.issuedTo < DateTime.Now)
                {
                    ViewBag.Message = "Issued To must be greater than '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
                    return View();
                }
                existingBook.borrowedBy = user.email;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                helper.InsertLog(user.email, "User borrowed Book: " + existingBook.name);
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
            book existingBook = db.books.Find(book.id);
            if (ModelState.IsValid)
            {
                existingBook.isActive = true;
                existingBook.issuedFrom = null;
                existingBook.issuedTo = null;
                existingBook.borrowedBy = null;
                db.Entry(existingBook).State = EntityState.Modified;
                db.SaveChanges();
                helper.InsertLog(username, "User returned Book: " + existingBook.name);
                return RedirectToAction("MyBooks");
            }
            return View(existingBook);
        }

        // GET: Book/DueDateExpired
        [Authorize(Roles = "admin")]
        public ActionResult DueDateExpired(string searchString)
        {
            if (searchString == null)
            {
                var dueDateExpiredBooks = db.books.Where(b => b.issuedTo < DateTime.Now);

                return View(dueDateExpiredBooks.ToList());
            }
            else
            {
                var books = db.books.Where(b => b.name.Contains(searchString) && b.issuedTo < DateTime.Now);
                return View(books);
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
