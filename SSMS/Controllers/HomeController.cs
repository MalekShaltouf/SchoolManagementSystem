using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index1()
        {
            SSMSDataContext context = new SSMSDataContext();

            ViewBag.Users = new SelectList(context.Users, "UserId", "FullName");
            return View("Index1");

        }
        [HttpPost]
        public void Index1(string Users) {
            Response.Write(Users);
        }
        public ViewResult Home()
        {
            return View();
        }
    }
}