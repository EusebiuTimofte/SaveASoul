using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Contexts;
using Save_A_Soul.Models;
using Save_A_Soul.DTOs;
using SaveASoul.Cors;

namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowCrossSite]
    public class SheltersController : ControllerBase
    {
        private readonly Context _context;

        public SheltersController(Context context)
        {
            _context = context;
        }

        // GET: api/Shelters
        [HttpGet]
        public async Task<JsonResult> GetShelters()
        {
            var shelters =  await _context.Shelters.ToListAsync();

            List<ShelterDTO> sheltersReturn = new List<ShelterDTO>();

            foreach(Shelter shelter in shelters)
            {
                ShelterDTO tempShelter = new ShelterDTO
                {
                    Id = shelter.Id,
                    Name = shelter.Name,
                    AddressId = (shelter.Address != null) ? shelter.Address.Id : 0,
                    Description = shelter.Description,
                    BankAccout = shelter.BankAccout
                };
                sheltersReturn.Add(tempShelter);
            }

            return new JsonResult(sheltersReturn);
        }

        // GET: api/Shelters/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetShelter(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);

            if (shelter == null)
            {
                return NotFound(String.Format("No shelter with id = {0} found", id));
            }

            ShelterDTO tempShelter = new ShelterDTO
            {
                Id = shelter.Id,
                Name = shelter.Name,
                AddressId = shelter.Address.Id,
                Description = shelter.Description,
                BankAccout = shelter.BankAccout
            };

            return new JsonResult(tempShelter);
        }

        // PUT: api/Shelters/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<JsonResult> PutShelter(int id, ShelterDTO shelter)
        {
            if (id != shelter.Id)
            {
                return new JsonResult("url parameter and body field id must have the same value");
            }

            if (_context.Shelters.Find(shelter.Id) == null)
            {
                return new JsonResult("Wrong shelter id");
            }

            Shelter shelterForDB = _context.Shelters.Find(shelter.Id);


            shelterForDB.Id = shelter.Id;
            shelterForDB.Name = shelter.Name;
            shelterForDB.Address = _context.Addresses.Find(shelter.AddressId);
            shelterForDB.Description = shelter.Description;
            shelterForDB.BankAccout = shelter.BankAccout;

            _context.Entry(shelterForDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelterExists(id))
                {
                    return new JsonResult(String.Format("No shelter with id {0} found in database", id));
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(shelter);
        }

        // POST: api/Shelters
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<JsonResult> PostShelter(ShelterDTO shelter)
        {
           

            Shelter shelterForDB = new Shelter
            {
                Name = shelter.Name,
                Address = _context.Addresses.Find(shelter.AddressId),
                Description = shelter.Description,
                BankAccout = shelter.BankAccout
            };

            _context.Shelters.Add(shelterForDB);
            await _context.SaveChangesAsync();
            shelter.Id = shelterForDB.Id;
            return new JsonResult(shelter);
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShelterDTO>> DeleteShelter(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }

            ShelterDTO shelterReturn = new ShelterDTO
            {
                Id = shelter.Id,
                Name = shelter.Name,
                AddressId = shelter.Address.Id,
                Description = shelter.Description,
                BankAccout = shelter.BankAccout
            };

            _context.Shelters.Remove(shelter);
            await _context.SaveChangesAsync();

            return shelterReturn;
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelters.Any(e => e.Id == id);
        }
    }
}
