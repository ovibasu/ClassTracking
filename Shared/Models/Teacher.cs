using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTracking.Shared.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage ="Teacher Name is Required?")]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IEnumerable<AssignTeacher> AssignTeachers { get; set; }
    }
}
