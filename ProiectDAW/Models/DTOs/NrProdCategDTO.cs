using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models.DTOs
{
    public class NrProdCategDTO
    {
        public ICollection<List<(string Categorie, int Nr)>> Categorii { get; set; }
    }
}
