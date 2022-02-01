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
    public class ComandaController : ControllerBase
    {
        private IComandaRepository comandaRepository;
        private IListaProduseRepository listaProduseRepository;
        private IProdusRepository produsRepository;
        private IUserRepository userRepository;
        private IJWTUtils jwtUtils;
        public ComandaController(IComandaRepository comanda, IListaProduseRepository listaProduse, IProdusRepository produs, IUserRepository user, IJWTUtils jwt)
        {
            comandaRepository = comanda;
            listaProduseRepository = listaProduse;
            produsRepository = produs;
            userRepository = user;
            jwtUtils = jwt;
        }

        [HttpGet("{id}")]
        public IActionResult GetComanda(Guid id)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Userul nu este autentificat." });
            }
            var comanda = userRepository.GetComandaByUser(user.Id, id);
            if (comanda == null)
            {
                return BadRequest(new { Message = "Nu exista aceasta comanda pentru acest user." });
            }
            var comandaDTO = new ComandaDTO();
            foreach (var com in comanda)
            {
                comandaDTO.Numar = id;
                comandaDTO.Data = com[0].Data;
                comandaDTO.ListaProduse = new List<ProdusComandaDTO>();
                foreach (var prod in com)
                {
                    var produsComandaDTO = new ProdusComandaDTO
                    {
                        Nume = prod.Nume,
                        Pret = prod.Pret,
                        Cantitate = prod.Cantitate
                    };
                    comandaDTO.ListaProduse.Add(produsComandaDTO);
                }

            }
            return Ok(comandaDTO);
        }
        [HttpPost]
        public IActionResult PostComanda(ComandaNouaDTO dto)
        {
            var user = (User)HttpContext.Items["User"];
            if (user == null)
            {
                return BadRequest(new { Message = "Userul nu este autentificat." });
            }
            var comanda = new Comanda
            {
                Data = DateTime.Now,
                UserId = user.Id
            };
            comandaRepository.Create(comanda);
            var rez = comandaRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Comanda esuata." });
            }
            foreach (var produs in dto.ListaProduse)
            {
                var prod = produsRepository.GetByNume(produs.Nume);
                var lista = new ListaProduse
                {
                    Cantitate = produs.Cantitate,
                    ComandaId = comanda.Id,
                    ProdusId = prod.Id
                };
                listaProduseRepository.Create(lista);
                rez = listaProduseRepository.Save();
                if (!rez)
                {
                    return BadRequest(new { Message = "Comanda esuata." });
                }
            }
            return Ok(new { Message = "Comanda plasata cu succes." });
        }

    }
}
