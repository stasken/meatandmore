using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Models
{
    public class UserRoles : IdentityUserRole<string>
    {
        public User User { get; set; } // Navigation property
        public Role Role { get; set; } // Navigation property
    }
}
