using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LogService;
using LogService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackendServices.Test
{
    public class ServicesTests : IDisposable
    {

        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected ServicesTests()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ClaimsContext));
                        services.AddDbContext<ClaimsContext>(options => { options.UseInMemoryDatabase("Claims"); });
                    });
                });

            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<ActionResult<IEnumerable<Logs>>> GetLogsAsync()
        {
            var response = await TestClient.GetAsync("https://localhost:44306/api/logs");
            return (await response.Content.ReadAsAsync<IEnumerable<Logs>>()).ToList();
        }


        protected async Task<Logs> CreateLogAsync(Logs log)
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44306/api/logs", log);
            return (await response.Content.ReadAsAsync<Logs>());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44306/api/JWT", new IdentityService.Models.UserInfo
            {
                Login = "elsa",
                Password = "password"
            });

            var registrationResponse = await response.Content.ReadAsAsync<string>();
            return registrationResponse;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ClaimsContext>();
         //   context.Database.EnsureDeleted();
        }
    }
}

