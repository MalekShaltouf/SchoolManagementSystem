using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        SSMSDataContext context = new SSMSDataContext();
        public ActionResult StudentsClass(int? id)
        {//this id represent ClassId
            if (id == null)
            { //if user enter Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not
                    List<Student> studentList = context.Students.Where(stu => stu.ClassId == id).ToList();
                    ViewBag.ClassId = id;
                    if (studentList == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(studentList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult Marks(int? id,int? ClassId) {//here id represent StudetId
            if (id == null || ClassId == null)
            { //if user enter Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not

                    ViewBag.StudentMaterials = context.Materials.Where(mat => mat.ClassId == ClassId).ToList();
                    List<Mark> StudentMark = context.Marks.Where(m => m.StudentId == id).ToList();

                    if (StudentMark == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(StudentMark);
                }
            }
            return RedirectToAction("Home", "Home");
        }
    }
}