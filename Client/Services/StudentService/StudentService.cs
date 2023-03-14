using ClassTracking.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _http;

        public StudentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/Students/{id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new Student { Name = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<Student>();
            }
        }

        public async Task<int> GetEnrollmentCountForStudent(int studentId)
        {
            return await _http.GetFromJsonAsync<int>($"api/Students/GetEnrollmentCountForStudent/{studentId}");
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _http.GetFromJsonAsync<Student>($"api/Students/{id}");
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _http.GetFromJsonAsync<List<Student>>("api/Students");
        }

        public async Task<int> GetTotalStudentInClass(int classId)
        {
            return await _http.GetFromJsonAsync<int>($"api/Students/GetTotalStudentInClass/Count/{classId}");
        }

        public async Task<Student> PostStudent(Student student)
        {
            var result = await _http.PostAsJsonAsync($"api/Students", student);
            return await result.Content.ReadFromJsonAsync<Student>();
        }

        public async Task PutStudent(int id, Student student)
        {
            var result = await _http.PutAsJsonAsync($"api/Students/{id}", student);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                new Student { Name = message };
            }
            else
            {
                await result.Content.ReadFromJsonAsync<Student>();
            }
        }
    }
}
