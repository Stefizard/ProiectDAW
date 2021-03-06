using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;

namespace ProiectDAW.Repositories
{
    public interface IProdusRepository:IGenericRepository<Produs>
    {
        Produs GetByNume(string nume);

        ICollection<Tuple<string, int>> GetNrProduseDinCategorii();
    }
}
