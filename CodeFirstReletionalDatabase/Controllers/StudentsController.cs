using CodeFirstReletionalDatabase.Models;
using CodeFirstReletionalDatabase.ViewModels.ViewEdit;
using CodeFirstReletionalDatabase.ViewModels.ViewInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstReletionalDatabase.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext db = new SchoolDbContext();
        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.course = db.Courses.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentInputModel data)
        {
            if (ModelState.IsValid)
            {
                var ext = Path.GetExtension(data.Photo.FileName);
                var f = Guid.NewGuid() + ext;
                data.Photo.SaveAs($"{Server.MapPath($"~/Uploads")}/{f}");
                var student = new Student
                {
                    StudentName = data.StudentName,
                    AddmitionDate = data.AddmitionDate,
                    Phone = data.Phone,
                    Photo = f,
                    IsRegular=data.IsRegular,
                    CourseId=data.CourseId
                };
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(data);
        }
        public ActionResult Edit(int id)
        {
            var student = db.Students.First(x => x.StudentId == id);
            ViewBag.currentPhoto = student.Photo;
            ViewBag.course = db.Courses.ToList();
            return View(new StudentEditModel { 
                StudentId=student.StudentId,
                StudentName=student.StudentName,
                AddmitionDate=student.AddmitionDate,
                Phone=student.Phone,
                CourseId=student.CourseId,
                IsRegular=student.IsRegular
            });
        }
        [HttpPost]
        public ActionResult Edit(StudentEditModel data)
        {
            var student = db.Students.First(x => x.StudentId == data.StudentId);
            if (ModelState.IsValid)
            {
                if(data.Photo != null)
                {
                    var ext = Path.GetExtension(data.Photo.FileName);
                    var f = Guid.NewGuid() + ext;
                    data.Photo.SaveAs($"{Server.MapPath($"~/Uploads")}/{f}");
                    student.Photo = f;
                }


                student.StudentName = data.StudentName;
                student.AddmitionDate = data.AddmitionDate;
                student.Phone = data.Phone;
                student.IsRegular = data.IsRegular;
                student.CourseId = data.CourseId;
               
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.currentPhoto = student.Photo;
            ViewBag.course = db.Courses.ToList();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            
            return View(db.Students.First(x => x.StudentId == id));
           
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {

            var s = new Student { StudentId = id };
            db.Entry(s).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

    }
}