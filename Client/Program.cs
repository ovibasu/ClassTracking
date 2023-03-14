using Blazored.Toast;
using ClassTracking.Client.Services.AssignTeacherService;
using ClassTracking.Client.Services.ClassService;
using ClassTracking.Client.Services.EnrollStudentService;
using ClassTracking.Client.Services.StudentService;
using ClassTracking.Client.Services.TeacherService;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassTracking.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("ClassTracking.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ClassTracking.ServerAPI"));

            builder.Services.AddBlazoredToast();
            builder.Services.AddMudServices();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IEnrollStudentService, EnrollStudentService>();
            builder.Services.AddScoped<IAssignTeacherService, AssignTeacherService>();

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
