using System;

namespace SchoolManagement.Data
{
    public class Schedule : Entity
    {
        public ClassRoom ClassRoom { get; set; }
        public Course Course { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
