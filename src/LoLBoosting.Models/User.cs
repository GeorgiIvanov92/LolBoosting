using System;
using System.Collections.Generic;
using LoLBoosting.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace LoLBoosting.Entities
{
    public class User : IdentityUser, IDeletableEntity
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new HashSet<IdentityUserRole<string>>();
        }

        public decimal Balance { get; set; }
        public double Winrate { get; set; }
        //public byte[] ImageData { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        //Deletable 
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
