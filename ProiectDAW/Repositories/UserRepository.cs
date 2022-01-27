using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;


namespace ProiectDAW.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context C) : base(C)
        {

        }
    }
}
