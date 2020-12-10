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
using IdentityService;
using IdentityService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackendServices.Test
{
    public class UserInfoServicesTests : IDisposable
    {

        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected UserInfoServicesTests()
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

        protected async Task<ActionResult<IEnumerable<UserInfo>>> GetLogsAsync()
        {
            var response = await TestClient.GetAsync("https://localhost:44362/api/userinfo/");
            return (await response.Content.ReadAsAsync<IEnumerable<UserInfo>>()).ToList();
        }


        protected async Task<UserInfo> CreateUserAsync(UserInfo log)
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44362/api/userinfo/", log);
            return (await response.Content.ReadAsAsync<UserInfo>());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44362/api/JWT/", new UserInfo
            {
                Name="",
                Surname="",
                Login = "elisa",
                Password = "password",
                RoleId=2,
                UserId=2
            });

            var registrationResponse = await response.Content.ReadAsStringAsync();
             return registrationResponse.ToString();
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ClaimsContext>();
         //   context.Database.EnsureDeleted();
        }
    }
}

