using System.Collections.Generic;

namespace SchoolManagement.Data
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }



    }
}
