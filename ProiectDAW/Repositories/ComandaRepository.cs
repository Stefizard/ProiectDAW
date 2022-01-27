using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;


namespace ProiectDAW.Repositories
{
    public class ComandaRepository : GenericRepository<Comanda>,IComandaRepository
    {
        public ComandaRepository (Context C):base(C)
        {

        }
    }
}
