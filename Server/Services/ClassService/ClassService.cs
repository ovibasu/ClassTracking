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

namespace ClassTracking.Server.Services.ClassService
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public IConfiguration Configuration { get; set; }

        public ClassService(ApplicationDbContext context, IUserService userService, IConfiguration Config)
        {
            _context = context;
            _userService = userService;
            Configuration = Config;
        }

        public async Task<Class> DeleteClass(int id)
        {
            var cls = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();
            return cls;
        }

        public async Task<Class> GetClass(int id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task<ViewClass> GetClassInfo(int classId)
        {
            ViewClass cls = new ViewClass();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = @"SELECT cls.ClassId, cls.[Name], cls.Standard, COUNT(*) AS TotalStudent, cls.MaxStudent, cls.SessionYear  
                            FROM Classes cls
                            INNER JOIN EnrollStudents es ON cls.ClassId = es.ClassId
                            WHERE cls.ClassId = @ClassId
                            GROUP BY cls.ClassId, cls.[Name], cls.[Standard], cls.MaxStudent, cls.SessionYear";
                        cmd.Parameters.AddWithValue("@ClassId", classId);
                        cmd.CommandTimeout = 0;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            if (reader["ClassId"] == DBNull.Value)
                            {
                                cls.ClassId = 0;
                            }
                            else
                            {
                                cls.ClassId = Convert.ToInt32(reader["ClassId"]);
                            }
                            if (reader["Name"] == DBNull.Value)
                            {
                                cls.Name = "";
                            }
                            else
                            {
                                cls.Name = reader["Name"].ToString();
                            }
                            if (reader["Standard"] == DBNull.Value)
                            {
                                cls.Standard = "";
                            }
                            else
                            {
                                cls.Standard = reader["Standard"].ToString();
                            }
                            if (reader["TotalStudent"] == DBNull.Value)
                            {
                                cls.TotalStudent = 0;
                            }
                            else
                            {
                                cls.TotalStudent = Convert.ToInt32(reader["TotalStudent"]);
                            }
                            if (reader["MaxStudent"] == DBNull.Value)
                            {
                                cls.MaxStudent = 0;
                            }
                            else
                            {
                                cls.MaxStudent = Convert.ToDecimal(reader["MaxStudent"]);
                            }
                            if (reader["SessionYear"] == DBNull.Value)
                            {
                                cls.SessionYear = 0;
                            }
                            else
                            {
                                cls.SessionYear = Convert.ToDecimal(reader["SessionYear"]);
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
            return cls;
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await _context.Classes.Include(u => u.EnrollStudents)
                                         .Include(a => a.AssignTeachers)
                                         .OrderBy(c => c.Name)
                                         .ToListAsync();
        }

        public async Task<Class> PostClass(Class cls)
        {
            cls.CreatedBy = _userService.GetUserId();
            cls.CreatedDate = DateTime.UtcNow;
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();

            return cls;
        }

        public async Task PutClass(int id, Class cls)
        {
            cls.UpdatedBy = _userService.GetUserId();
            cls.UpdatedDate = DateTime.UtcNow;
            _context.Entry(cls).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool ClassExists(int id)
        {
            return _context.Classes.Any(m => m.ClassId == id);
        }
    }
}
