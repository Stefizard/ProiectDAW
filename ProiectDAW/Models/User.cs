using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public enum rol { Admin, User}
    public class User:BaseEntity
    {
        public rol Rol { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public Guid CredentialeId { get; set; }
        public virtual Credentiale Credentiale { get; set; }
        public virtual ICollection<Comanda> Comenzi { get; set; }

    }
}
