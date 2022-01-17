using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Models.Settings
{
    public class AdminAccountInformation
    {
        public AccountSettings Admin { get; set; }
        public AccountSettings Teacher { get; set; }
        public class AccountSettings
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
