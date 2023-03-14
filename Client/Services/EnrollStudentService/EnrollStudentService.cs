using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.EnrollStudentService
{
    public class EnrollStudentService : IEnrollStudentService
    {
        private readonly HttpClient _http;

        public EnrollStudentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<EnrollStudent> DeleteEnrollStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/EnrollStudents/{id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new EnrollStudent { CreatedBy = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<EnrollStudent>();
            }
        }

        public async Task<EnrollStudent> GetEnrollStudent(int id)
        {
            return await _http.GetFromJsonAsync<EnrollStudent>($"api/EnrollStudents/{id}");
        }

        public async Task<IEnumerable<EnrollStudent>> GetEnrollStudents()
        {
            return await _http.GetFromJsonAsync<List<EnrollStudent>>("api/EnrollStudents");
        }

        public async Task<IEnumerable<ViewStudent>> GetStudentInfo(int id)
        {
            return await _http.GetFromJsonAsync<List<ViewStudent>>($"api/EnrollStudents/GetStudentInfo/{id}");
        }

        public async Task<EnrollStudent> PostEnrollStudent(EnrollStudent enroll)
        {
            var result = await _http.PostAsJsonAsync($"api/EnrollStudents", enroll);
            return await result.Content.ReadFromJsonAsync<EnrollStudent>();
        }

        public async Task PutEnrollStudent(int id, EnrollStudent enroll)
        {
            var result = await _http.PutAsJsonAsync($"api/EnrollStudents/{id}", enroll);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                new EnrollStudent { CreatedBy = message };
            }
            else
            {
                await result.Content.ReadFromJsonAsync<EnrollStudent>();
            }
        }
    }
}
