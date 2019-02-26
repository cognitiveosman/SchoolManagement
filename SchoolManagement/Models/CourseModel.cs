using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class CourseModel
    {


        public int Id { get; set; }

        [DisplayName("Course Name")]
        [Required]
        public string CourseName { get; set; }

        [DisplayName("Course Code")]
        [Required]
        public string CourseCode { get; set; }

        [DisplayName("Enrolled Students")]
        public IEnumerable<string> Students { get; set; }

    }
}
