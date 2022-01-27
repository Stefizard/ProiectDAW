using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public class Comanda:BaseEntity
    {
        public DateTime Data { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<ListaProduse> ListeProduse { get; set; }
    }
}
