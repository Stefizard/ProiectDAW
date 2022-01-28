using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;
using ProiectDAW.Data;
using Microsoft.EntityFrameworkCore;


namespace ProiectDAW.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context C) : base(C)
        {

        }

        public User GetByEmail(string email)
        {
            return _table.Include(x => x.Credentiale).FirstOrDefault(x => x.Credentiale.Email.Equals(email));
        }
    }
}
