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

namespace ClassTracking.Server.Services.EnrollStudentService
{
    public class EnrollStudentService : IEnrollStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public IConfiguration Configuration { get; set; }

        public EnrollStudentService(ApplicationDbContext context, IUserService userService, IConfiguration Config)
        {
            _context = context;
            _userService = userService;
            Configuration = Config;
        }

        public async Task<EnrollStudent> DeleteEnrollStudent(int id)
        {
            var enroll = await _context.EnrollStudents.FindAsync(id);
            _context.EnrollStudents.Remove(enroll);
            await _context.SaveChangesAsync();
            return enroll;
        }

        public async Task<EnrollStudent> GetEnrollStudent(int id)
        {
            return await _context.EnrollStudents.FindAsync(id);
        }

        public async Task<IEnumerable<ViewStudent>> GetStudentInfo(int classId)
        {

            List<ViewStudent> students = new List<ViewStudent>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = @"SELECT s.StudentId, s.[Name], s.FatherName, s.MotherName, s.Nationality, CONVERT(DATE, s.EnrollDate) AS EnrollDate 
                            FROM EnrollStudents es
                            INNER JOIN Students s ON es.StudentId = s.StudentId
                            WHERE es.ClassId = @ClassId";
                        cmd.Parameters.AddWithValue("@ClassId", classId);
                        cmd.CommandTimeout = 0;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            ViewStudent student = new ViewStudent();
                            if (reader["StudentId"] == DBNull.Value)
                            {
                                student.StudentId = 0;
                            }
                            else
                            {
                                student.StudentId = Convert.ToInt32(reader["StudentId"]);
                            }
                            if (reader["Name"] == DBNull.Value)
                            {
                                student.Name = "";
                            }
                            else
                            {
                                student.Name = reader["Name"].ToString();
                            }
                            if (reader["FatherName"] == DBNull.Value)
                            {
                                student.FatherName = "";
                            }
                            else
                            {
                                student.FatherName = reader["FatherName"].ToString();
                            }
                            if (reader["MotherName"] == DBNull.Value)
                            {
                                student.MotherName = "";
                            }
                            else
                            {
                                student.MotherName = reader["MotherName"].ToString();
                            }
                            if (reader["Nationality"] == DBNull.Value)
                            {
                                student.Nationality = "";
                            }
                            else
                            {
                                student.Nationality = reader["Nationality"].ToString();
                            }
                            if (reader["EnrollDate"] == DBNull.Value)
                            {
                                student.EnrollDate = DateTime.UtcNow;
                            }
                            else
                            {
                                student.EnrollDate = Convert.ToDateTime(reader["EnrollDate"]);
                            }
                            students.Add(student);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
            return students;
        }

        public async Task<IEnumerable<EnrollStudent>> GetEnrollStudents()
        {
            return await _context.EnrollStudents.Include(s=>s.Student)
                                                .Include(c=>c.Class)
                                                .ToListAsync();
        }

        public async Task<EnrollStudent> PostEnrollStudent(EnrollStudent enroll)
        {
            enroll.CreatedBy = _userService.GetUserId();
            enroll.CreatedDate = DateTime.UtcNow;
            _context.EnrollStudents.Add(enroll);
            await _context.SaveChangesAsync();

            return enroll;
        }

        public async Task PutEnrollStudent(int id, EnrollStudent enroll)
        {
            enroll.UpdatedBy = _userService.GetUserId();
            enroll.UpdatedDate = DateTime.UtcNow;
            _context.Entry(enroll).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EnrollStudentExists(int id)
        {
            return _context.EnrollStudents.Any(m => m.EnrollStudentId == id);
        }
    }
}
