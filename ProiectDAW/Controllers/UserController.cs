using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Repositories;
using ProiectDAW.Models;
using ProiectDAW.Models.DTOs;
using BCryptNet = BCrypt.Net.BCrypt;
using ProiectDAW.Utility;

namespace ProiectDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;
        private ICredentialeRepository credentialeRepository;
        private IJWTUtils jwtUtils;
        public UserController(IUserRepository user, ICredentialeRepository cred, IJWTUtils jwt)
        {
            userRepository = user;
            credentialeRepository = cred;
            jwtUtils = jwt;
        }

        [HttpGet("{id}")]
        [Authorization(Rol.Admin)]
        public IActionResult GetUser(Guid id)
        {
            var user = userRepository.Get(id);
            if (user == null)
            {
                return BadRequest(new { Message = "Nu exista acest user." });
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetMyUser()
        {
            var user = (User)HttpContext.Items["User"];
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
                Parola = BCryptNet.HashPassword(user.Parola)
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
                Telefon = user.Telefon,
                Rol = Rol.User
            };
            userRepository.Create(newUser);
            rez = userRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Inregistrare esuata." });
            }
            return Ok(new { Message = "Inregistrare efectuata cu succes." });
        }

        [HttpPost("Autentificare")]
        public IActionResult Login(CredentialeDTO cred)
        {
            var user = userRepository.GetByEmail(cred.Email);
            if (user == null || !BCryptNet.Verify(cred.Parola, user.Credentiale.Parola))
            {
                return BadRequest(new { Message = "Autentificare esuata." });
            }
            var token = jwtUtils.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPut]
        public IActionResult UpdateUser(UserDTO newUser)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Nu exista acest user." });
            }
            user.Nume = newUser.Nume;
            user.Prenume = newUser.Prenume;
            user.Telefon = newUser.Telefon;
            userRepository.Update(user);
            var rez = userRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Update esuat." });
            }
            return Ok(new { Message = "Update efectuat cu succes." });
        }

        [HttpDelete]
        public IActionResult DeleteUser()
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Nu exista acest user." });
            }
            var cred = credentialeRepository.Get(user.CredentialeId);
            if (cred != null)
            {
                credentialeRepository.Delete(cred);
                credentialeRepository.Save();
            }
            userRepository.Delete(user);
            var rez = userRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Delete esuat." });
            }
            return Ok(new { Message = "Delete efectuat cu succes." });
        }
    }
}
