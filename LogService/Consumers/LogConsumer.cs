using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;
using LogService.Models;
using Logs = LogService.Models.Logs;

namespace LogService.Consumers
{
    public class LogConsumer: IConsumer<Shared.Models.Logs>
    {
        private readonly Models.ClaimsContext _context;


        public LogConsumer(ClaimsContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<Shared.Models.Logs> context)
        {
            
            var data = context.Message;
            Logs log = new Logs
            {
                ActionPerformed = data.ActionPerformed,
                UserId = data.UserId,
                UserName = data.UserName,
                TimeStamp = DateTime.Now

            };
            _context.Add(log);
            await _context.SaveChangesAsync();


        }
    }
}
