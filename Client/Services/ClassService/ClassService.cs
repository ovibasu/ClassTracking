using ClassTracking.Shared.Models;
using ClassTracking.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassTracking.Client.Services.ClassService
{
    public class ClassService : IClassService
    {
        private readonly HttpClient _http;

        public ClassService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Class> DeleteClass(int id)
        {
            var result = await _http.DeleteAsync($"api/Classes/{id}");

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return new Class { Name = message };
            }
            else
            {
                return await result.Content.ReadFromJsonAsync<Class>();
            }
        }

        public async Task<Class> GetClass(int id)
        {
            return await _http.GetFromJsonAsync<Class>($"api/Classes/{id}");
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await _http.GetFromJsonAsync<List<Class>>("api/Classes");
        }

        public async Task<ViewClass> GetClassInfo(int id)
        {
            return await _http.GetFromJsonAsync<ViewClass>($"api/Classes/GetClassInfo/{id}");
        }

        public async Task<Class> PostClass(Class cls)
        {
            var result = await _http.PostAsJsonAsync($"api/Classes", cls);
            return await result.Content.ReadFromJsonAsync<Class>();
        }

        public async Task PutClass(int id, Class cls)
        {
            var result = await _http.PutAsJsonAsync($"api/Classes/{id}", cls);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = await result.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                new Class { Name = message };
            }
            else
            {
                await result.Content.ReadFromJsonAsync<Class>();
            }
        }
    }
}
