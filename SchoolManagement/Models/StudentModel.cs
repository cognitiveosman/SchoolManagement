using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class StudentModel
    {


        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]

        public string LastName { get; set; }

        [DisplayName("Person Number")]
        [Required]


        public string PersonNumber { get; set; }

        [DisplayName("Enrolled Course(s) Code(s)")]
        public string[] Courses { get; set; }

        public List<SelectListItem> AllCourses { get; set; }

    }
}
