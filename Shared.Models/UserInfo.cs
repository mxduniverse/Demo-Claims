﻿using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
    }
}
