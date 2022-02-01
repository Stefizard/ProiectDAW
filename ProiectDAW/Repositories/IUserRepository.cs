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

        ICollection<List<(DateTime Data, int Cantitate, string Nume, float Pret, string Categorie)>> GetComandaByUser(Guid idUser, Guid idComanda);

    }
}
