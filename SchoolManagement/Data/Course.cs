using System.Collections.Generic;

namespace SchoolManagement.Data
{
    public class Course : Entity
    {

        public string CourseName { get; set; }
        public string CourseCode { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }
}
