using System;
using System.Collections.Generic;

namespace Logs.Services.Models

{
    public partial class Logs
    {
        public int? LogId { get; set; }
        public byte[] TimeStamp { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string ActionPerformed { get; set; }
    }
}
