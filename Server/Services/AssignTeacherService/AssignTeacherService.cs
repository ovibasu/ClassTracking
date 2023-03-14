using ClassTracking.Server.Data;
using ClassTracking.Server.Services.UserService;
using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Services.AssignTeacherService
{
    public class AssignTeacherService : IAssignTeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public IConfiguration Configuration { get; set; }

        public AssignTeacherService(ApplicationDbContext context, IUserService userService, IConfiguration Config)
        {
            _context = context;
            _userService = userService;
            Configuration = Config;
        }

        public async Task<AssignTeacher> DeleteAssignTeacher(int id)
        {
            var assign = await _context.AssignTeachers.FindAsync(id);
            _context.AssignTeachers.Remove(assign);
            await _context.SaveChangesAsync();
            return assign;
        }

        public async Task<AssignTeacher> GetAssignTeacher(int id)
        {
            return await _context.AssignTeachers.FindAsync(id);
        }

        public async Task<ViewTeacher> GetTeacherInfo(int classId)
        {
            ViewTeacher teacher = new ViewTeacher();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = @"SELECT t.TeacherId, t.[Name], t.Designation
                            FROM Teachers t
                            INNER JOIN AssignTeachers at ON t.TeacherId = at.TeacherId
                            WHERE at.ClassId = @ClassId";
                        cmd.Parameters.AddWithValue("@ClassId", classId);
                        cmd.CommandTimeout = 0;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            if (reader["TeacherId"] == DBNull.Value)
                            {
                                teacher.TeacherId = 0;
                            }
                            else
                            {
                                teacher.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                            }
                            if (reader["Name"] == DBNull.Value)
                            {
                                teacher.Name = "";
                            }
                            else
                            {
                                teacher.Name = reader["Name"].ToString();
                            }
                            if (reader["Designation"] == DBNull.Value)
                            {
                                teacher.Designation = "";
                            }
                            else
                            {
                                teacher.Designation = reader["Designation"].ToString();
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
            return teacher;
        }

        public async Task<IEnumerable<AssignTeacher>> GetAssignTeachers()
        {
            return await _context.AssignTeachers.Include(u => u.Teacher)
                                                .Include(a => a.Class)
                                                .ToListAsync();
        }

        public async Task<AssignTeacher> PostAssignTeacher(AssignTeacher assign)
        {
            assign.CreatedBy = _userService.GetUserId();
            assign.CreatedDate = DateTime.UtcNow;
            _context.AssignTeachers.Add(assign);
            await _context.SaveChangesAsync();

            return assign;
        }

        public async Task PutAssignTeacher(int id, AssignTeacher assign)
        {
            assign.UpdatedBy = _userService.GetUserId();
            assign.UpdatedDate = DateTime.UtcNow;
            _context.Entry(assign).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool AssignTeacherExists(int id)
        {
            return _context.AssignTeachers.Any(m => m.AssignTeacherId == id);
        }
    }
}
