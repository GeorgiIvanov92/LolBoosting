using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolBoosting.Models
{
    public class User : IdentityUser
    {
        public IdentityRole Role  { get; set; }
    }
}
