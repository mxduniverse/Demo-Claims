using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityService.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using GreenPipes;

namespace IdentityService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly ClaimsContext _context;
        private readonly IBus _bus;

        public UserInfoController(ClaimsContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        // GET: api/UserInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo( int? skip, int? take)
        {

            var usersinfo = _context.UserInfo.AsQueryable();

     


            if (skip != null)
            {
                usersinfo = usersinfo.Skip((int)skip);
            }

            if (take != null)
            {
                usersinfo = usersinfo.Take((int)take);
            }

            logAction ("Access all the users");

            return await usersinfo.ToListAsync();


        }

        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var userInfo = await _context.UserInfo.FindAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

            logAction ("Get user information for userid " + id);

            return userInfo;
        }

        // PUT: api/UserInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo userInfo)
        {
            if (id != userInfo.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            logAction("Update User information usreid: " + id);
            return NoContent();
        }

        // POST: api/UserInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostUserInfo(UserInfo userInfo)
        {
            _context.UserInfo.Add(userInfo);
            await _context.SaveChangesAsync();
            logAction("Insert user Information ");
            return CreatedAtAction("GetUserInfo", new { id = userInfo.UserId }, userInfo);
        }

        // DELETE: api/UserInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(int id)
        {
            var userInfo = await _context.UserInfo.FindAsync(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            _context.UserInfo.Remove(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
        }

        private bool UserInfoExists(int id)
        {
            return _context.UserInfo.Any(e => e.UserId == id);
        }


        private async void logAction(string ActionPerformed)
        {

            var UserName = "No user";
            var UserId = "0";

            try
            {
                UserName = User.Claims.First(i => i.Type == "UserName").Value;
                UserId = User.Claims.First(i => i.Type == "Id").Value;
            }
            catch
            {

            }

            Shared.Models.Logs log = new Shared.Models.Logs
            {
                ActionPerformed = ActionPerformed,
                UserId = int.Parse(UserId),
                UserName = UserName
            };



            log.TimeStamp = DateTime.Now;
            Uri uri = new Uri("rabbitmq://localhost/logsQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(log);

        }
    }
}
