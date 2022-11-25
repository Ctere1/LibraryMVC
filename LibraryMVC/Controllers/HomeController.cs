using LibraryMVC.Models;
using System;
using System.Collections.Generic;
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


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}