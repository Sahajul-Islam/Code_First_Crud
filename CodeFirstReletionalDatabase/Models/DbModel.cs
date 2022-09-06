using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirstReletionalDatabase.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new List<Student>();
        }
        public int CourseId { get; set; }
        [Required,StringLength(50),Display(Name ="Course")]
        public string CourseName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string StudentName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Add. Date")]
        public DateTime AddmitionDate { get; set; }
        [Required]
        public bool IsRegular { get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required,StringLength(200)]
        public string Photo { get; set; }
        [Required,ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SchoolDbContext>());
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}