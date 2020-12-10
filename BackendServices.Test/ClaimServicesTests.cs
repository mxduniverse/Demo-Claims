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
using ClaimService;
using ClaimService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackendServices.Test
{
    public class ClaimServicesTests : IDisposable
    {

        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected ClaimServicesTests()
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

        protected async Task<ActionResult<IEnumerable<Claims>>> GetLogsAsync()
        {
            var response = await TestClient.GetAsync("https://localhost:44398/api/claims/");
            return (await response.Content.ReadAsAsync<IEnumerable<Claims>>()).ToList();
        }


        protected async Task<Claims> CreateClaimAsync(Claims log)
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44398/api/claims/", log);
            return (await response.Content.ReadAsAsync<Claims>());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("https://localhost:44362/api/JWT/", new UserInfo
            {
                Name="",
                Surname="",
                Login = "elisa",
                Password = "password",
                RoleId=0,
                UserId=0
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

