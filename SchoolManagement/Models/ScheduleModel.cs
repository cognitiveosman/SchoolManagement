using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class ScheduleModel
    {

        public int Id { get; set; }


        [DisplayName("Class Room")]
        [Required]
        public string ClassRoom { get; set; }

        [DisplayName("Course")]
        [Required]

        public string CourseCode { get; set; }

        [DisplayName("Start Time")]
        [Required]


        public DateTime StartTime { get; set; } = DateTime.Now;

        [DisplayName("End Time")]
        [Required]

        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(2);


        public List<SelectListItem> AllClassRooms { get; set; }
        public List<SelectListItem> AllCourses { get; set; }

    }
}
