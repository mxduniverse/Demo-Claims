using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Consumers
{
    public class CheckUserConsumer : IConsumer<CheckUserStatus>
    {
        private readonly Models.ClaimsContext _context;

        public CheckUserConsumer(ClaimsContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<CheckUserStatus> context)
        {
            var User = await _context.UserInfo.FirstOrDefaultAsync(u => u.Login == context.Message.UserName && u.Password == context.Message.Password);
            if (User == null)
                await context.RespondAsync<UserStatusResult>(new
                {
                    UserId =0,
                    Name ="",
                    Surname = "",
                    Login = "",
                    RoleId =0

                });
            else
            await context.RespondAsync<UserStatusResult>(new
            {
            User.UserId,
                User.Name ,
                User.Surname ,
                User.Login ,
                User.RoleId 

    });
        }


    }
}
