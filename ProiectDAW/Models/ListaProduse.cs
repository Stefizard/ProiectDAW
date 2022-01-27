using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public class ListaProduse:BaseEntity
    {
        public int Cantitate { get; set; }
        public Guid ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        public Guid ProdusId { get; set; }
        public Produs Produs { get; set; }
    }
}
