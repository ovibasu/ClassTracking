using ClassTracking.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient _http;

        public TeacherService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Teacher> DeleteTeacher(int id)
        {
            var result = await _http.DeleteAsync($"api/Teachers/{id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new Teacher { Name = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<Teacher>();
            }
        }

        public async Task<int> GetAssignTecher(int teacherId)
        {
            return await _http.GetFromJsonAsync<int>($"api/Teachers/GetAssignTecher/{teacherId}");
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await _http.GetFromJsonAsync<Teacher>($"api/Teachers/{id}");
        }

        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
        }

        public async Task<int> GetTotalTeacherInClass(int classId)
        {
            return await _http.GetFromJsonAsync<int>($"api/Teachers/GetTotalTeacherInClass/Count/{classId}");
        }

        public async Task<Teacher> PostTeacher(Teacher teacher)
        {
            var result = await _http.PostAsJsonAsync($"api/Teachers", teacher);
            return await result.Content.ReadFromJsonAsync<Teacher>();
        }

        public async Task PutTeacher(int id, Teacher teacher)
        {
            var result = await _http.PutAsJsonAsync($"api/Teachers/{id}", teacher);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                new Teacher { Name = message };
            }
            else
            {
                await result.Content.ReadFromJsonAsync<Teacher>();
            }
        }
    }
}
