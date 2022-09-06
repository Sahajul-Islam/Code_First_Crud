using CodeFirstReletionalDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstReletionalDatabase.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDbContext db = new SchoolDbContext();
        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Course c)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Courses.First(x=>x.CourseId==id));
        }
        [HttpPost]
        public ActionResult Edit(Course c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Courses.First(x=>x.CourseId==id));
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var c = new Course { CourseId = id };
            db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            
        }
    }
}