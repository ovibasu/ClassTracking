using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.ClassService
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetClasses();
        Task<Class> GetClass(int id);
        Task<ViewClass> GetClassInfo(int classId);
        Task<Class> PostClass(Class cls);
        Task PutClass(int id, Class cls);
        Task<Class> DeleteClass(int id);
    }
}
