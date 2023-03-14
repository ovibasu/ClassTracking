using ClassTracking.Server.Data;
using ClassTracking.Server.Services.UserService;
using ClassTracking.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public TeacherService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Teacher> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<int> GetAssignTecher(int teacherId)
        {
            return await _context.AssignTeachers.CountAsync(e => e.TeacherId == teacherId);
        }

        public async Task<int> GetTotalTeacherInClass(int classId)
        {
            return await _context.AssignTeachers.CountAsync(e => e.ClassId == classId);
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _context.Teachers.Include(a => a.AssignTeachers)
                                         .OrderBy(c => c.Name)
                                         .ToListAsync();
        }

        public async Task<Teacher> PostTeacher(Teacher teacher)
        {
            teacher.CreatedBy = _userService.GetUserId();
            teacher.CreatedDate = DateTime.UtcNow;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        public async Task PutTeacher(int id, Teacher teacher)
        {
            teacher.UpdatedBy = _userService.GetUserId();
            teacher.UpdatedDate = DateTime.UtcNow;
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool TeacherExists(int id)
        {
            return _context.Teachers.Any(m => m.TeacherId == id);
        }
    }
}
