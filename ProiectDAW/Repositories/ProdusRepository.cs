using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;


namespace ProiectDAW.Repositories
{
    public class ProdusRepository : GenericRepository<Produs>, IProdusRepository
    {
        public ProdusRepository(Context C) : base(C)
        {

        }

        public Produs GetByNume(string nume)
        {
            return _table.FirstOrDefault(x => x.Nume.Equals(nume));
        }

        public ICollection<Tuple<string, int>> GetNrProduseDinCategorii()
        {
            var result = from x in _context.Produse
                         group x by x.Categorie into g
                         select new
                         {
                             Nume = g.Key,
                             Nr = g.Count()
                         };

            var lista = new List<Tuple<string, int>>();

            foreach (var x in result)
            {
                var tuplu = new Tuple<string, int>(x.Nume, x.Nr);
                lista.Add(tuplu);
            }

            return lista;
        }
    }
}
