using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Save_A_Soul.Contexts;
using Save_A_Soul.DTOs;
using Save_A_Soul.Models;
namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AddressesController : ControllerBase
    {
        private readonly Context _context;

        public AddressesController(Context context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            var addresses = await _context.Addresses.ToListAsync();

            List<AddressDTO> dto = new List<AddressDTO>();

            foreach(var address in addresses)
            {
                dto.Add(new AddressDTO
                {
                    Id = address.Id,
                    City = address.City,
                    Street = address.Street,
                    StreetNumber = address.StreetNumber
                });
            }

            return new JsonResult(dto);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return new JsonResult(new AddressDTO
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                StreetNumber = address.StreetNumber
            });
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAddress(int id, AddressDTO dto)
        {   
           

            if (id != dto.Id)
            {
                return BadRequest();
            }

            Address address = new Address
            {
                Id = dto.Id,
                City = dto.City,
                Street = dto.Street,
                StreetNumber = dto.StreetNumber
            };

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostAddress(AddressDTO addressdto)
        {
            Address address = new Address
            {
               Id=addressdto.Id,
               City=addressdto.City,
               Street=addressdto.Street,
               StreetNumber=addressdto.StreetNumber
            };

            EntityEntry<Address> add = _context.Addresses.Add(address);

            await _context.SaveChangesAsync();
            //TODO: returneaza id=0
            //_context.Entry(add).State = EntityState.Modified;
            // return CreatedAtAction("GetAnimal", new { id = add.Entity.Id }, animal);
            address.Id = addressdto.Id;
            return new JsonResult(address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return new JsonResult(address);
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
