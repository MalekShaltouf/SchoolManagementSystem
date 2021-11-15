using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        SSMSDataContext context = new SSMSDataContext();
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName,string Password)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.SingleOrDefault(u => u.UserName == UserName && u.Password == Password);
                if (user != null)
                {
                    Session["UserName"] = user.UserName;
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewBag.LoginStatus = "username or password not correct.";
                }
            }
            return View();
        }
        public RedirectToRouteResult LogOut()
        {
            Session.Remove("UserName");
            return RedirectToAction("Home", "Home");
        }
    }
}