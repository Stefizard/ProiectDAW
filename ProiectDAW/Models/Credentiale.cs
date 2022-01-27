using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public class Credentiale : BaseEntity
    {
        public string Email { get; set; }
        public string Parola { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
