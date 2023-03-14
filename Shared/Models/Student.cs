using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTracking.Shared.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage ="Student Name is Required?")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Father Name is Required?")]
        [StringLength(100)]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Mother Name is Required?")]
        [StringLength(100)]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Nationality is Required?")]
        [StringLength(100)]
        public string Nationality { get; set; }
        public DateTime EnrollDate { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IEnumerable<EnrollStudent> EnrollStudents { get; set; }
    }
}
