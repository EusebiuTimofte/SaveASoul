using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Contexts;
using Save_A_Soul.Models;

namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionsController : ControllerBase
    {
        private readonly Context _context;

        public AdoptionsController(Context context)
        {
            _context = context;
        }

        // GET: api/Adoptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adoption>>> GetAdoptions()
        {
            return await _context.Adoptions.ToListAsync();
        }

        // GET: api/Adoptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adoption>> GetAdoption(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);

            if (adoption == null)
            {
                return NotFound();
            }

            return adoption;
        }

        // PUT: api/Adoptions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoption(int id, Adoption adoption)
        {
            if (id != adoption.AnimalId)
            {
                return BadRequest();
            }

            _context.Entry(adoption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionExists(id))
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

        // POST: api/Adoptions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Adoption>> PostAdoption(Adoption adoption)
        {
            _context.Adoptions.Add(adoption);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdoptionExists(adoption.AnimalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdoption", new { id = adoption.AnimalId }, adoption);
        }

        // DELETE: api/Adoptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Adoption>> DeleteAdoption(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }

            _context.Adoptions.Remove(adoption);
            await _context.SaveChangesAsync();

            return adoption;
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoptions.Any(e => e.AnimalId == id);
        }
    }
}
