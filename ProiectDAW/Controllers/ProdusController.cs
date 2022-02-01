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
    public class ProdusController : ControllerBase
    {
        private IProdusRepository produsRepository;
        private IJWTUtils jwtUtils;
        public ProdusController(IProdusRepository produs, IJWTUtils jwt)
        {
            produsRepository = produs;
            jwtUtils = jwt;
        }

        [HttpGet("{produs}")]
        public IActionResult GetProdus(string produs)
        {
            var prod = produsRepository.GetByNume(produs);
            if (prod == null)
            {
                return BadRequest(new { Message = "Nu exista acest produs." });
            }
            return Ok(prod);
        }

        [HttpPost]
        [Authorization(Rol.Admin)]
        public IActionResult PostProdus(ProdusDTO produs)
        {
            var newProdus = new Produs
            {
                Nume = produs.Nume,
                Pret = produs.Pret,
                Categorie = produs.Categorie,
                Descriere = produs.Descriere,
                Stoc = produs.Stoc
            };
            produsRepository.Create(newProdus);
            var rez = produsRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Adaugarea produsului esuata." });
            }
            return Ok(new { Message = "Adaugarea produsului efectuata cu succes." });
        }

        [HttpPut("{produs}")]
        [Authorization(Rol.Admin)]
        public IActionResult UpdateProdus(string produs, ProdusDTO newProdus)
        {
            var prod = produsRepository.GetByNume(produs);
            if (prod == null)
            {
                return BadRequest(new { Message = "Nu exista acest produs." });
            }
            prod.Nume = newProdus.Nume;
            prod.Pret = newProdus.Pret;
            prod.Categorie = newProdus.Categorie;
            prod.Descriere = newProdus.Descriere;
            prod.Stoc = newProdus.Stoc;
            produsRepository.Update(prod);
            var rez = produsRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Update-ul produsului esuat." });
            }
            return Ok(new { Message = "Update-ul produsului efectuat cu succes." });
        }

        [HttpDelete("{produs}")]
        [Authorization(Rol.Admin)]
        public IActionResult DeleteProdus(string produs)
        {
            var prod = produsRepository.GetByNume(produs);
            if (prod == null)
            {
                return BadRequest(new { Message = "Nu exista acest produs." });
            }
            produsRepository.Delete(prod);
            var rez = produsRepository.Save();
            if (!rez)
            {
                return BadRequest(new { Message = "Delete esuat." });
            }
            return Ok(new { Message = "Delete efectuat cu succes." });
        }

    }
}
