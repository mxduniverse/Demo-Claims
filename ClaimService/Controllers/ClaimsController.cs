using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimService.Models;
using MassTransit;
using GreenPipes;
using System.Security.Claims;

namespace ClaimService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimsContext _context;
        private readonly IBus _bus;

        public ClaimsController(ClaimsContext context, IBus bus)
        {
            _context = context;
            _bus = bus;

        }

        // GET: api/Claims
        /// <summary>
        /// Get All the Claims
        /// </summary>
        /// <remarks>
        /// To return a claims specific to a user, you can filter by userid or 
        /// add search for a claim
        ///
        /// </remarks>
        /// <param name="userid">Optional. Use to filter claims by user</param>
        /// <param name="description">Optional. Use to search claims based on the description</param>
        /// <returns>A newly created Claim</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claims>>> GetClaims(int? userid, string? description)
        {

            var claims = _context.Claims.AsQueryable();

            if (userid != null)
            {
                claims = claims.Where(u => u.UserId == userid);
            }

            if (description != null)
            {
                claims = claims.Where(u => u.Description.Contains(description));
            }


            logAction("GetClaims -"+ HttpContext.Request.Path+ HttpContext.Request.QueryString );

            return await claims.ToListAsync();
        }

        // GET: api/Claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Claims>> GetClaims(int id)
        {
            var claims = await _context.Claims.FindAsync(id);

            if (claims == null)
            {
                return NotFound();
            }

            logAction("GetClaims id "+ id+ " -" + HttpContext.Request.Path + HttpContext.Request.QueryString);

            return claims;
        }

        // PUT: api/Claims/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaims(int id, Claims claims)
        {
            if (id != claims.ClaimId)
            {
                return BadRequest();
            }

            _context.Entry(claims).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            logAction("Put claim id " + id + " -" + HttpContext.Request.Path + HttpContext.Request.QueryString);

            return NoContent();
        }

        // POST: api/Claims
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Claims>> PostClaims(Claims claims)
        {
            _context.Claims.Add(claims);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClaims", new { id = claims.ClaimId }, claims);
        }

        // DELETE: api/Claims/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Claims>> DeleteClaims(int id)
        {
            var claims = await _context.Claims.FindAsync(id);
            if (claims == null)
            {
                return NotFound();
            }

            _context.Claims.Remove(claims);
            await _context.SaveChangesAsync();

            logAction("Add claim -" + HttpContext.Request.Path + HttpContext.Request.QueryString);

            return claims;
        }

        private bool ClaimsExists(int id)
        {
            return _context.Claims.Any(e => e.ClaimId == id);
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
