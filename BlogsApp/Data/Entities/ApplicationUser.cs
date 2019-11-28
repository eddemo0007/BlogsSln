using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(64)]
        public string LastName { get; set; }
    }
}
