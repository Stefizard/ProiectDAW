﻿using System;
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
    }
}
