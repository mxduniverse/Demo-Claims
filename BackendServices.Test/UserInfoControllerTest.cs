using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using IdentityService.Models;
using FluentAssertions;

namespace BackendServices.Test
{
    public class UserInfoControllerTest: UserInfoServicesTests
    {
        [Fact]
        public async Task GetAll()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("https://localhost:44362/api/userinfo/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<UserInfo>>()).ToList().Should().NotBeEmpty();
        }


        [Fact]
        public async Task Get_Users_WhenLogExists()
        {
            // Arrange
            await AuthenticateAsync();
            var createdUser = await CreateUserAsync(new UserInfo
            {
                Name = "test",
                Surname = "test",
                Login = "test",
                Password = "password",
                RoleId = 0,
                UserId = 0

            });

            // Act
            var response = await TestClient.GetAsync("https://localhost:44362/api/userinfo/" + createdUser.UserId.ToString());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returneduser = await response.Content.ReadAsAsync<UserInfo>();
            returneduser.UserId.Should().Be(createdUser.UserId);
            returneduser.Login.Should().Be("test");
        }


        [Fact]
        public async Task Update_User()
        {
            // Arrange
                await AuthenticateAsync();
            var createdUser = await CreateUserAsync(new UserInfo
            {
                Name = "test2",
                Surname = "test2",
                Login = "test2",
                Password = "password",
                RoleId = 2,
                UserId = 0

            });



            // Act
            createdUser.Login = createdUser.Login+ createdUser.UserId.ToString();


            var response = await TestClient.PutAsJsonAsync("https://localhost:44362/api/userinfo/" + createdUser.UserId.ToString(),createdUser);



            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    
        }


    }
}
