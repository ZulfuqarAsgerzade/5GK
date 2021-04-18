using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel.Improter.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Pass { get; set; }
    }
}
