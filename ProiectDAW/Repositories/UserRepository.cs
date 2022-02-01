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

        public ICollection<List<(DateTime Data, int Cantitate, string Nume, float Pret, string Categorie)>> GetComandaByUser(Guid idUser, Guid idComanda)
        {
            var result = from x in _context.Useri
                         join y in _context.Comenzi on x.Id equals y.UserId
                         join z in _context.ListeProduse on y.Id equals z.ComandaId
                         join a in _context.Produse on z.ProdusId equals a.Id
                         where x.Id == idUser && y.Id == idComanda
                         select new
                         {
                             Data = y.Data,
                             Cantitate = z.Cantitate,
                             Nume = a.Nume,
                             Pret = a.Pret,
                             Categorie = a.Categorie
                         };
            var lista = new List<List<(DateTime Data, int Cantitate, string Nume, float Pret, string Categorie)>>();
            foreach (var x in result)
            {
                var prod = new List<(DateTime Data, int Cantitate, string Nume, float Pret, string Categorie)>();
                prod.Add((x.Data,x.Cantitate,x.Nume, x.Pret,x.Categorie));
                lista.Add(prod);
            }
            return lista;
        }
    }
}
