using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        SSMSDataContext context = new SSMSDataContext();

        [ActionName("Profile")]
        public ActionResult Pprofile() //
        {
            if (Session["UserName"] != null)
            {
                TeacherAuthoriz teacherAuthorize = new TeacherAuthoriz();
                StudentAuthorize studentAuthorize = new StudentAuthorize();
                if (teacherAuthorize.isUserTeacher(Session["UserName"].ToString()) || studentAuthorize.isUserStudent(Session["UserName"].ToString())) //here if the user is teacher or student so will execute the code that include this if-statement
                {
                    string UserName = Session["UserName"].ToString();
                    int UserId = context.Users.SingleOrDefault(u => u.UserName == UserName).UserId;
                    ViewBag.Profile = context.Users.SingleOrDefault(u => u.UserId == UserId);

                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult ChangePassword() {
            if (Session["UserName"] != null)
            {
                TeacherAuthoriz teacherAuthorize = new TeacherAuthoriz();
                StudentAuthorize studentAuthorize = new StudentAuthorize();
                if (teacherAuthorize.isUserTeacher(Session["UserName"].ToString()) || studentAuthorize.isUserStudent(Session["UserName"].ToString()))//here if the user is teacher or student so will execute the code that include this if-statement
                {
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult ChangePassword(int id, string Password, string ConfirmPassword)
        {
            try
            {
                User user = new User()
                {
                    UserId = id,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };
                context.Users.Attach(user);
                context.Entry(user).Property(u => u.Password).IsModified = true;
                context.SaveChanges();
                ViewBag.SuccessfulStatus = "Update Successful ";
            }
            catch
            {
                ViewBag.FaildStatus = "Failed Update";
            }
            return View();
        }
    }
}