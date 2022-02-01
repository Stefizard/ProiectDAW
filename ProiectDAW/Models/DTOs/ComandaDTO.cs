using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models.DTOs
{
    public class ProdusComandaDTO
    {
        public string Nume { get; set; }
        public float Pret { get; set; }
        public int Cantitate { get; set; }
    }
    public class ComandaDTO
    {
        public Guid Numar { get; set; }
        public DateTime Data { get; set; }
        public ICollection<ProdusComandaDTO> ListaProduse { get; set; }
    }
}
