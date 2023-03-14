using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTracking.Shared.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        [Required(ErrorMessage ="Class Name is Required?")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Class Standard is Required?")]
        [StringLength(20)]
        public string Standard { get; set; }
        public decimal MaxStudent { get; set; }
        public decimal SessionYear { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IEnumerable<EnrollStudent> EnrollStudents { get; set; }
        public IEnumerable<AssignTeacher> AssignTeachers { get; set; }
    }
}
