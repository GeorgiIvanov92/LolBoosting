using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolBoosting.Models
{
    public class User : IdentityUser
    {
        public decimal Balance { get; set; }
        public double Winrate { get; set; }
        public byte[] ImageData { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
