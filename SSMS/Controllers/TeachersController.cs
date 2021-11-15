using SSMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSMS.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers
        SSMSDataContext context = new SSMSDataContext();
        public ActionResult TeacherClasses()
        {
            if (Session["UserName"] != null)
            { //check if user signIn or Not
                TeacherAuthoriz teacherAuthoriz = new TeacherAuthoriz();
                if (teacherAuthoriz.isUserTeacher(Session["UserName"].ToString()))
                { //Check If User is Teacher Or Not.
                    string TeacherUserName = Session["UserName"].ToString(); //return UserName for teacher (TeacherUserName).
                    int UserId = context.Users.SingleOrDefault(user => user.UserName == TeacherUserName).UserId; //return UserId for teacher.

                    int?[] ListClassId = context.Teachers.Where(teac => teac.UserId == UserId).Select(x => x.ClassId).ToArray();// store ClassId For Classes That Teacher Providing it.

                    ViewBag.MaterialName = context.Teachers.First(teac => teac.UserId == UserId).Material.MaterialNameInEnglish;
                    ViewBag.MaterialId = context.Teachers.First(teac => teac.UserId == UserId).Material.MaterialId;

                    List<Class> cclassList = new List<Class>();
                    for (int i = 0; i < ListClassId.Length; i++)
                    {
                        int? ClassId = ListClassId[i];
                        Class cclass = context.Classes.SingleOrDefault(c => c.ClassId == ClassId);
                        cclassList.Add(cclass);
                    }
                    return View(cclassList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult TeacherStudents(int? id, string MaterialName, int? MaterialId)//this id represent ClassId
        {
            if (id == null || MaterialName == null || MaterialId == null)
            { //if user enter DeleteUser Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                TeacherAuthoriz teacherAuthorize = new TeacherAuthoriz();
                if (teacherAuthorize.isUserTeacher(Session["UserName"].ToString()))
                { // check if user Admin or not
                    List<Student> ListStudent = context.Students.Where(stu => stu.ClassId == id).ToList();

                    ViewBag.MaterialName = MaterialName;
                    ViewBag.MaterialId = MaterialId;
                    ViewBag.ClassId = id;
                    
                    if (ListStudent == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(ListStudent);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddMark(int? id, int? StudentId, int? MaterialId)//here id represent ClassId 
        {
            if (id == null || StudentId == null || MaterialId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                TeacherAuthoriz teacherAuthorize = new TeacherAuthoriz();
                if (teacherAuthorize.isUserTeacher(Session["UserName"].ToString()))
                {
                    ViewBag.ClassId = id;
                    ViewBag.MaterialId = MaterialId;
                    ViewBag.StudentId = StudentId;
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ActionResult AddMark(Mark mark)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    mark.ClassId = int.Parse(RouteData.Values["id"].ToString());
                    context.Marks.Add(mark);
                    context.SaveChanges();
                    return RedirectToAction("TeacherStudents", "Teachers", new { id = mark.ClassId, MaterialId = mark.MaterialId, MaterialName = Request.QueryString["MaterialName"] });
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Adding";
                }
            }
            return View();
        }
        public ActionResult UpdateMark(int? id,int? Studentid,int? MaterialId) { //here id represent ClassId 
            if (id == null || Studentid == null || MaterialId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                TeacherAuthoriz teacherAuthorize = new TeacherAuthoriz();
                if (teacherAuthorize.isUserTeacher(Session["UserName"].ToString()))
                {
                    Mark mark = context.Marks.SingleOrDefault(m => m.StudentId == Studentid && m.ClassId == id && m.MaterialId == MaterialId);
                    return View(mark);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ActionResult UpdateMark(Mark mark) {
            if (ModelState.IsValid) {
                try
                {
                    context.Entry(mark).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("TeacherStudents", "Teachers", new { id = mark.ClassId, MaterialId = mark.MaterialId, MaterialName = Request.QueryString["MaterialName"] });
                }
                catch {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            return View();
        }
    }
}