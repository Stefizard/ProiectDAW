using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;


namespace ProiectDAW.Repositories
{
    public class ListaProduseRepository : GenericRepository<ListaProduse>, IListaProduseRepository
    {
        public ListaProduseRepository(Context C) : base(C)
        {

        }
    }
}
