using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excel.Improter.Models
{
    public class CreateUserInput
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public List<CheckRoles> Roles { get; set; }
    }

    public class CheckRoles
    {
        public string Name { get; set; }
        public string NormalName { get; set; }
        public bool IsSelected { get; set; }
    }
}
