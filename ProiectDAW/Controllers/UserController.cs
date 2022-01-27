using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Repositories;
using ProiectDAW.Models;
using ProiectDAW.Models.DTOs;

namespace ProiectDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;
        private ICredentialeRepository credentialeRepository;
        public UserController(IUserRepository user, ICredentialeRepository cred)
        {
            userRepository = user;
            credentialeRepository = cred;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = userRepository.Get(id);
            if (user == null)
            {
                return BadRequest(new { Message = "Nu exista acest user." });
            }
            return Ok(user);
        }

        [HttpPost("Inregistrare")]
        public IActionResult PostUser(UserDTO user)
        {
            var newCredentiale = new Credentiale
            {
                Email = user.Email,
                Parola = user.Parola
            };
            credentialeRepository.Create(newCredentiale);
            var rez = credentialeRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Inregistrare esuata." });
            }
            var newUser = new User
            {
                CredentialeId = newCredentiale.Id,
                Nume = user.Nume,
                Prenume = user.Prenume,
                Telefon = user.Telefon
            };
            userRepository.Create(newUser);
            rez = userRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Inregistrare esuata." });
            }
            return Ok(new { Message = "Inregistrare efectuata cu succes." });
        }
    }
}
