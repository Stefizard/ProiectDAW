using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models.DTOs
{
    public class ComandaNouaProdusDTO
    {
        public string Nume { get; set; }
        public int Cantitate { get; set; }
    }
    public class ComandaNouaDTO
    {
        public ICollection<ComandaNouaProdusDTO> ListaProduse { get; set; }
    }
}
