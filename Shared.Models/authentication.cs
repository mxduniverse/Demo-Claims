using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models
{
    public interface CheckUserStatus
    {
        string UserName { get; }
        string Password { get; }
    }

    public interface UserStatusResult
    {
        string UserId { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public int RoleId { get; set; }

        public string? token { get; set; }
    }
}
