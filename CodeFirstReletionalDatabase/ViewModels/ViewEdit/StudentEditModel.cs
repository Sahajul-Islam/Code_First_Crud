using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirstReletionalDatabase.ViewModels.ViewEdit
{
    public class StudentEditModel
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string StudentName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Add. Date")]
        public DateTime AddmitionDate { get; set; }
        [Required]
        public bool IsRegular { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        
        public HttpPostedFileBase Photo { get; set; }
        [Required, ForeignKey("Course")]
        public int CourseId { get; set; }
    }
}