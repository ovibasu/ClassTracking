using ClassTracking.Server.Data;
using ClassTracking.Server.Services.UserService;
using ClassTracking.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public StudentService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<int> GetEnrollmentCountForStudent(int studentId)
        {
            return await _context.EnrollStudents.CountAsync(e => e.StudentId == studentId);
        }

        public async Task<int> GetTotalStudentInClass(int classId)
        {
            return await _context.EnrollStudents.CountAsync(e => e.ClassId == classId);
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.Include(u => u.EnrollStudents)
                                         .OrderBy(c => c.Name)
                                         .ToListAsync();
        }

        public async Task<Student> PostStudent(Student student)
        {
            student.CreatedBy = _userService.GetUserId();
            student.CreatedDate = DateTime.UtcNow;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task PutStudent(int id, Student student)
        {
            student.UpdatedBy = _userService.GetUserId();
            student.UpdatedDate = DateTime.UtcNow;
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(m => m.StudentId == id);
        }
    }
}
