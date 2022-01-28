using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;

namespace ProiectDAW.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        User GetByEmail(string email);

    }
}
