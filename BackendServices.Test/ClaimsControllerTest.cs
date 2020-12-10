using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using ClaimService.Models;
using FluentAssertions;

namespace BackendServices.Test
{
    public class ClaimsControllerTest: ClaimServicesTests
    {
        [Fact]
        public async Task GetAll()
        {
            // Arrange
          //  await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("https://localhost:44398/api/claims/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<IEnumerable<Claims>>()).ToList().Should().NotBeEmpty();
        }


        [Fact]
        public async Task Get_Claim_WhenLogExists()
        {
            // Arrange
        //    await AuthenticateAsync();
            var createdClaim = await CreateClaimAsync(new Claims
            {
                DamagedItem = "DamagedItem",
                Address = "Test",
                Incidence = "Incidence Test",
                Date = DateTime.Now,
                UserId=1,
                Status="new",
                Description="abcd efg"
                
            });

            // Act
            var response = await TestClient.GetAsync("https://localhost:44398/api/claims/" + createdClaim.ClaimId.ToString());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedclaim = await response.Content.ReadAsAsync<Claims>();
            returnedclaim.ClaimId.Should().Be(createdClaim.ClaimId);
            returnedclaim.Status.Should().Be("new");
            returnedclaim.DamagedItem.Should().Be("DamagedItem");
        }


        [Fact]
        public async Task Update_Claim()
        {
            // Arrange
            //    await AuthenticateAsync();
            var createdClaim = await CreateClaimAsync(new Claims
            {
                DamagedItem = "DamangeItem",
                Address = "Test",
                Incidence = "Incidence Test",
                Date = DateTime.Now,
                UserId = 1,
                Status = "new",
                Description = "abcd efg"

            });



            // Act
            createdClaim.Status = "Closed";
            createdClaim.Description = "ok";

            var response = await TestClient.PutAsJsonAsync("https://localhost:44398/api/claims/" + createdClaim.ClaimId.ToString(),createdClaim);



            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    
        }


    }
}
