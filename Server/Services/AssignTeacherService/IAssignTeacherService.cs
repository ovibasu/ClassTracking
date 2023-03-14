using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Services.AssignTeacherService
{
    public interface IAssignTeacherService
    {
        Task<IEnumerable<AssignTeacher>> GetAssignTeachers();
        Task<AssignTeacher> GetAssignTeacher(int id);
        Task<ViewTeacher> GetTeacherInfo(int classId);
        Task<AssignTeacher> PostAssignTeacher(AssignTeacher assign);
        Task PutAssignTeacher(int id, AssignTeacher assign);
        Task<AssignTeacher> DeleteAssignTeacher(int id);
        bool AssignTeacherExists(int id);
    }
}
