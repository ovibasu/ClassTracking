using ClassTracking.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.StudentService
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<int> GetEnrollmentCountForStudent(int studentId);
        Task<int> GetTotalStudentInClass(int classId);
        Task<Student> PostStudent(Student student);
        Task PutStudent(int id, Student student);
        Task<Student> DeleteStudent(int id);
    }
}
