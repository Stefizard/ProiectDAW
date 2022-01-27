using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;


namespace ProiectDAW.Repositories
{
    public class CredentialeRepository : GenericRepository<Credentiale>, ICredentialeRepository
    {
        public CredentialeRepository(Context C) : base(C)
        {

        }
    }
}
