using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClaimService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using MassTransit;

namespace ClaimService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {

        public IConfiguration _configuration;
        private readonly ClaimsContext _context;
        private readonly IBus _bus;


        public JWTController(IConfiguration config, ClaimsContext context, IBus bus)
        {
            _configuration = config;
            _context = context;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.UserInfo _userData)
        {

            if (_userData != null && _userData.Login != null && _userData.Password != null)
            {


                var serviceAddress = new Uri("rabbitmq://localhost/check-user-status");
                var client = _bus.CreateRequestClient<CheckUserStatus>(serviceAddress);

                var Response = await client.GetResponse<UserStatusResult>(new { UserName = _userData.Login, Password = _userData.Password });

                var user = Response.Message;



                if (user != null)
                {


                    if (user.UserId != "0")
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FirstName", user.Name),
                    new Claim("UserName", user.Login),
                    new Claim("Role", user.RoleId.ToString())
                   };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                        user.token = new JwtSecurityTokenHandler().WriteToken(token);

                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest(new { message = "Invalid credentials" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Invalid credentials" });
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
        }

      /*  private async Task<UserInfo> GetUser(string login, string password)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }*/
    }

}
