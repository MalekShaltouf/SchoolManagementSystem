using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        SSMSDataContext context = new SSMSDataContext();
        public ActionResult StudentMarks() {
            
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                StudentAuthorize studentAuthorize = new StudentAuthorize();
                if (studentAuthorize.isUserStudent(Session["UserName"].ToString()))
                { // check if user Admin or not
                    string UserName = Session["UserName"].ToString();
                    int UserId = context.Users.SingleOrDefault(u => u.UserName == UserName).UserId;
                    Student stu = context.Students.SingleOrDefault(s => s.UserId == UserId);
                    int StudentId = stu.StudentID;
                    ViewBag.ClassName = context.Classes.SingleOrDefault(c => c.ClassId == stu.ClassId).ClassNameInEnglish;

                    List<Mark> studentMarks = context.Marks.Where(mark => mark.StudentId == StudentId).ToList();
                    if (studentMarks == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(studentMarks);
                }
            }
            return RedirectToAction("Home", "Home");
        }
    }
}