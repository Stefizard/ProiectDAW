using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models.DTOs
{
    public class ProdusDTO
    {
        public string Nume { get; set; }
        public float Pret { get; set; }
        public string Categorie { get; set; }
        public string Descriere { get; set; }
        public int Stoc { get; set; }
    }
}
