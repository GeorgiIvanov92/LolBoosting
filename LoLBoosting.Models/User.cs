using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LolBoosting.Models
{
    public class User : IdentityUser
    {
        public decimal Balance { get; set; }
        public double Winrate { get; set; }
        //public byte[] ImageData { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
