using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using LogService.Models;
using FluentAssertions;

namespace BackendServices.Test
{
    public class LogsControllerTest:ServicesTests
    {
        [Fact]
        public async Task GetAll_Logs()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("https://localhost:44306/api/logs");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<Logs>>()).ToList().Should().NotBeEmpty();
        }


        [Fact]
        public async Task Get_Log_WhenLogExists()
        {
            // Arrange
            await AuthenticateAsync();
            var createdLog = await CreateLogAsync(new Logs
            {

                ActionPerformed = "Test",
                UserName = "testusername",
                TimeStamp = DateTime.Now,
                UserId=1
                
            });

            // Act
            var response = await TestClient.GetAsync("https://localhost:44306/api/logs/"+createdLog.LogId.ToString());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedLog = await response.Content.ReadAsAsync<Logs>();
            returnedLog.LogId.Should().Be(createdLog.LogId);
            returnedLog.ActionPerformed.Should().Be("Test");
            returnedLog.UserName.Should().Be("testusername");
        }


    }
}
