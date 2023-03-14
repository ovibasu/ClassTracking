using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.EnrollStudentService
{
    public interface IEnrollStudentService
    {
        Task<IEnumerable<EnrollStudent>> GetEnrollStudents();
        Task<EnrollStudent> GetEnrollStudent(int id);
        Task<IEnumerable<ViewStudent>> GetStudentInfo(int classId);
        Task<EnrollStudent> PostEnrollStudent(EnrollStudent enroll);
        Task PutEnrollStudent(int id, EnrollStudent enroll);
        Task<EnrollStudent> DeleteEnrollStudent(int id);
    }
}
