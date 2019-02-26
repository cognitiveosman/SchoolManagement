using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class ClassRoomModel
    {

        public int Id { get; set; }

        [DisplayName("ClassRoom Code")]
        [Required]
        public string Code { get; set; }

    }
}
