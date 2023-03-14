using ClassTracking.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<Teacher> GetTeacher(int id);
        Task<int> GetAssignTecher(int teacherId);
        Task<int> GetTotalTeacherInClass(int classId);
        Task<Teacher> PostTeacher(Teacher teacher);
        Task PutTeacher(int id, Teacher teacher);
        Task<Teacher> DeleteTeacher(int id);
    }
}
