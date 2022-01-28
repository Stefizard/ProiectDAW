using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Models;

namespace ProiectDAW.Utility
{
    public interface IJWTUtils
    {
        public string GenerateToken(User user);
        public Guid ValidateToken(string token);
    }
}
