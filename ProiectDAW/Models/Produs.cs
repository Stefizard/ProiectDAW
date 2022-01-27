using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public class Produs:BaseEntity
    {
        public string Nume { get; set; }
        public float Pret { get; set; }
        public string Categorie { get; set; }
        public string Descriere { get; set; }
        public int Stoc { get; set; }
        public virtual ICollection<ListaProduse> ListeProduse { get; set; }
    }
}
