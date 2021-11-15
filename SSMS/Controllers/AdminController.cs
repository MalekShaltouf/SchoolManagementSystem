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
    public class AdminController : Controller
    {
        // GET: Admin
        SSMSDataContext context = new SSMSDataContext();

        /*[Start]
         * Admin Create,Read,Update,Delete For Users
         */
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<User> UserList = context.Users.ToList();
                    return View(UserList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddUser()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserType = user.UserType.ToLower();
                    context.Users.Add(user);
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Registration Successful";
                }
                catch
                {
                    ViewBag.FaildStatus = "Registration Faild";
                }
            }
            ModelState.Clear();
            return View();

        }
        public ActionResult UpdateUser(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    User user = context.Users.SingleOrDefault(u => u.UserId == id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserType = user.UserType.ToLower();
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Successful Update";
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult DeleteUser(int? Id)
        {
            if (Id == null) { //if user enter DeleteUser Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null) { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString())) { // check if user Admin or not
                    User user = context.Users.SingleOrDefault(u => u.UserId == Id);
                    if (user == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DeleteUser_Post(int Id)
        {
            try
            {
                User user = context.Users.SingleOrDefault(u => u.UserId == Id);
                context.Users.Remove(user);
                context.SaveChanges();
            }
            catch
            {
                ViewBag.DeleteStatus = "The deletion failed";
            }
            List<User> userList = context.Users.ToList();

            return View("index", userList);
        }
        /*[End]
         * Admin Create,Read,Update,Delete For User
         */

        /*[Start]
         * Admin Create,Read,Update,Delete For Classes
         */

        public ActionResult Classes()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<Class> cclass = context.Classes.ToList();
                    return View(cclass);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddClass()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult AddClass(Class cclass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Classes.Add(cclass);
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Registration Successful";
                }
                catch
                {
                    ViewBag.FaildStatus = "Registration Faild";
                }
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult UpdateClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    Class cclass = context.Classes.SingleOrDefault(c => c.ClassId == id);
                    if (cclass == null)
                    {
                        return HttpNotFound();
                    }
                    return View(cclass);

                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult UpdateClass(Class cclass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Entry(cclass).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Successful Update";
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            return View();
        }
        public ActionResult DeleteClass(int? id)
        {
            if (id == null)
            { //if user enter Delete Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not
                    Class cclass = context.Classes.SingleOrDefault(c => c.ClassId == id);
                    if (cclass == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(cclass);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        [ActionName("DeleteClass")]
        public ViewResult DeleteClass_Post(int id)
        {
            try
            {
                Class cclass = context.Classes.SingleOrDefault(c => c.ClassId == id);
                context.Classes.Remove(cclass);
                context.SaveChanges();
            }
            catch
            {
                ViewBag.FaildStatus = "The deletion failed";
            }
            List<Class> ListClasses = context.Classes.ToList();
            return View("Classes", ListClasses);
        }
        
        /*[End]
         * Admin Create,Read,Update,Delete For Classes
         */

        /*[Start]
         * Admin Create,Read,Update,Delete For Materials
         */
        public ActionResult Materials()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<Material> MaterialList = context.Materials.ToList();
                    return View(MaterialList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddMaterials()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult AddMaterials(Material material)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Materials.Add(material);
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Registration Successful";
                }
                catch
                {
                    ViewBag.FaildStatus = "Registration Faild";
                }
            }
            ModelState.Clear();
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();

        }
        public ActionResult UpdateMaterial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    Material material = context.Materials.SingleOrDefault(m => m.MaterialId == id);
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    if (material == null)
                    {
                        return HttpNotFound();
                    }
                    return View(material);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult UpdateMaterial(string MaterialNameInArabic, string MaterialNameInEnglish)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int MaterialId = int.Parse(RouteData.Values["id"].ToString());
                    Material material = context.Materials.SingleOrDefault(m => m.MaterialId == MaterialId);

                    material.MaterialNameInArabic = MaterialNameInArabic;
                    material.MaterialNameInEnglish = MaterialNameInEnglish;

                    context.Entry(material).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Successful Update";
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();
        }
        public ActionResult DeleteMaterial(int? id)
        {
            if (id == null)
            { //if user enter Delete Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not
                    Material material = context.Materials.SingleOrDefault(m => m.MaterialId == id);
                    if (material == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(material);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        [ActionName("DeleteMaterial")]
        public ViewResult DeleteMaterial_Post(int? id)
        {
            try
            {
                Material material = context.Materials.SingleOrDefault(m => m.MaterialId == id);
                context.Materials.Remove(material);
                context.SaveChanges();
            }
            catch
            {
                ViewBag.FaildStatus = "The deletion failed";
            }
            List<Material> ListMaterial = context.Materials.ToList();
            return View("Materials", ListMaterial);
        }

        /*[End]
         * Admin Create,Read,Update,Delete For Materials
         */

        /*[Start]
         * Admin Create,Read,Update,Delete For Teachers
         */
        public ActionResult Teachers()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<Teacher> teacherList = context.Teachers.ToList();
                    return View(teacherList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddTeacher()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    ViewBag.MaterialId = new SelectList(context.Materials, "MaterialId", "MaterialNameInEnglish");
                    ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "teacher"), "UserId", "FullName");
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult AddTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    context.Teachers.Add(teacher);
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Registration Successful";
                }
                catch
                {
                    ViewBag.FaildStatus = "Registration Faild";
                }
            }
            ModelState.Clear();
            ViewBag.MaterialId = new SelectList(context.Materials, "MaterialId", "MaterialNameInEnglish");
            ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "teacher"), "UserId", "FullName");
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();
        }
        public ActionResult UpdateTeacher(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    Teacher teacher = context.Teachers.SingleOrDefault(teat => teat.TeacherId == id);

                    ViewBag.MaterialId = new SelectList(context.Materials, "MaterialId", "MaterialNameInEnglish");
                    ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "teacher"), "UserId", "FullName");
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    if (teacher == null)
                    {
                        return HttpNotFound();
                    }
                    return View(teacher);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult UpdateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Entry(teacher).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Successful Update";
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            ViewBag.MaterialId = new SelectList(context.Materials, "MaterialId", "MaterialNameInEnglish");
            ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "teacher"), "UserId", "FullName");
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();
        }
        public ActionResult DeleteTeacher(int? id)
        {
            if (id == null)
            { //if user enter DeleteUser Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not
                    Teacher teacher = context.Teachers.SingleOrDefault(teat => teat.TeacherId == id);
                    if (teacher == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(teacher);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        [ActionName("DeleteTeacher")]
        public ViewResult DeleteTeacher_Post(int id)
        {
            try
            {
                Teacher teacher = context.Teachers.SingleOrDefault(teat => teat.TeacherId == id);
                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
            catch
            {
                ViewBag.DeleteStatus = "The deletion failed";
            }
            List<Teacher> teacherList = context.Teachers.ToList();

            return View("Teachers", teacherList);
        }
        /*[End]
         * Admin Create,Read,Update,Delete For Teachers
         */

        /*[Start]
         * Admin Create,Read,Update,Delete For Students
         */
        public ActionResult Students()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<Student> studentList = context.Students.ToList();
                    return View(studentList);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        public ActionResult AddStudent()
        {
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    List<User> AllStudentList = context.Users.Where(user => user.UserType == "student").ToList(); //here we return all Users that has UserType = student(return All student in Users table)
                    List<User> StudentThatNotHaveClass = new List<User>(); //here will store the users(student) that have not class
                    foreach (User user in AllStudentList) {
                        int UserId = user.UserId;
                        if (!(context.Students.Any(stu => stu.UserId == UserId))) {//this check mean => if the student not regesiter in class => so execute the code that include this if-statement
                            StudentThatNotHaveClass.Add(user);
                        }
                    }
                    //ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "student"), "UserId", "UserId");
                    ViewBag.UserId = new SelectList(StudentThatNotHaveClass, "UserId", "FullName");
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    return View();
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Registration Successful";
                }
                catch
                {
                    ViewBag.FaildStatus = "Registration Faild";
                }
            }
            ModelState.Clear();
            ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "student"), "UserId", "UserId");
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();
        }
        public ActionResult UpdateStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            {
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                {
                    Student Student = context.Students.SingleOrDefault(stu => stu.StudentID == id);
                    ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "student"), "UserId", "FullName");
                    ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
                    if (Student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(Student);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ViewResult UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Entry(student).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewBag.SuccessfulStatus = "Successful Update";
                }
                catch
                {
                    ViewBag.FaildStatus = "Faild Update";
                }
            }
            ViewBag.UserId = new SelectList(context.Users.Where(user => user.UserType == "student"), "UserId", "UserId");
            ViewBag.ClassId = new SelectList(context.Classes, "ClassId", "ClassNameInEnglish");
            return View();
        }
        public ActionResult DeleteStudent(int? id)
        {
            if (id == null)
            { //if user enter DeleteUser Page without enter id => so will return to user BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserName"] != null)
            { //check if user Sign In Or Not
                AdminAuthorize adminAuthorize = new AdminAuthorize();
                if (adminAuthorize.IsUserAdmin(Session["UserName"].ToString()))
                { // check if user Admin or not
                    Student student = context.Students.SingleOrDefault(stu => stu.StudentID == id);
                    if (student == null) //if user enter id not exists in Db => so will return NotFound Page
                    {
                        return HttpNotFound();
                    }
                    return View(student);
                }
            }
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        [ActionName("DeleteStudent")]
        public ViewResult DeleteStudent_Post(int id)
        {
            try
            {
                Student student = context.Students.SingleOrDefault(stu => stu.StudentID == id);
                context.Students.Remove(student);
                context.SaveChanges();
            }
            catch
            {
                ViewBag.DeleteStatus = "The deletion failed";
            }
            List<Student> studentList = context.Students.ToList();

            return View("Students", studentList);
        }
        /*[End]
         * Admin Create,Read,Update,Delete For Students
         */

        public JsonResult IsTeacherHasOneMaterial(int UserId)
        {
            return Json(!context.Students.Any(user => user.UserId == UserId), JsonRequestBehavior.AllowGet);
        }
    }
}