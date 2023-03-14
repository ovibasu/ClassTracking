using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.AssignTeacherService
{
    public class AssignTeacherService : IAssignTeacherService
    {
        private readonly HttpClient _http;

        public AssignTeacherService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AssignTeacher> DeleteAssignTeacher(int id)
        {
            var result = await _http.DeleteAsync($"api/AssignTeachers/{id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new AssignTeacher { CreatedBy = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<AssignTeacher>();
            }
        }

        public async Task<AssignTeacher> GetAssignTeacher(int id)
        {
            return await _http.GetFromJsonAsync<AssignTeacher>($"api/AssignTeachers/{id}");
        }

        public async Task<IEnumerable<AssignTeacher>> GetAssignTeachers()
        {
            return await _http.GetFromJsonAsync<List<AssignTeacher>>("api/AssignTeachers");
        }

        public async Task<ViewTeacher> GetTeacherInfo(int id)
        {
            return await _http.GetFromJsonAsync<ViewTeacher>($"api/AssignTeachers/GetTeacherInfo/{id}");
        }

        public async Task<AssignTeacher> PostAssignTeacher(AssignTeacher assign)
        {
            var result = await _http.PostAsJsonAsync($"api/AssignTeachers", assign);
            return await result.Content.ReadFromJsonAsync<AssignTeacher>();
        }

        public async Task PutAssignTeacher(int id, AssignTeacher assign)
        {
            var result = await _http.PutAsJsonAsync($"api/AssignTeachers/{id}", assign);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                new AssignTeacher { CreatedBy = message };
            }
            else
            {
                await result.Content.ReadFromJsonAsync<AssignTeacher>();
            }
        }
    }
}
