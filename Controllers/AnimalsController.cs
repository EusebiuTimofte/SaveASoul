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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SaveASoul.Cors;

namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AnimalsController : ControllerBase
    {
        private readonly Context _context;
        public AnimalsController(Context context)
        {
            _context = context;
        }

        // GET: api/Animals 
        [HttpGet]
        public JsonResult GetAnimals()
        {
            var animals = _context.Animals.ToList();

            List<AnimalDTO> dto = new List<AnimalDTO>();

            foreach (Animal animal in animals)
            {
                AnimalDTO _dto = new AnimalDTO
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    Age = animal.Age,
                    Species = animal.Species,
                    Breed = animal.Breed,
                    Photo = animal.Photo,
                    Description = animal.Description,
                    Weight = animal.Weight,
                    ShelterId = (animal.Shelter != null) ? animal.Shelter.Id : 0
                };

                dto.Add(_dto);
            }
            JsonResult jsonAnimal = new JsonResult(dto);

            return jsonAnimal;
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            AnimalDTO dto = new AnimalDTO
            {
                Id = animal.Id,
                Name = animal.Name,
                Age = animal.Age,
                Species = animal.Species,
                Breed = animal.Breed,
                Photo = animal.Photo,
                Description = animal.Description,
                Weight = animal.Weight,
                ShelterId = animal.Shelter.Id
            };
            

            return new JsonResult(dto);
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, AnimalDTO animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            Animal animalForDB = new Animal()
            {
                Id = animal.Id,
                Name = animal.Name,
                Age = animal.Age,
                Species = animal.Species,
                Breed = animal.Breed,
                Photo = animal.Photo,
                Description = animal.Description,
                Weight = animal.Weight,
                Shelter = _context.Shelters.Find(animal.ShelterId)
            };

            _context.Entry(animalForDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<JsonResult> PostAnimal(AnimalDTO animal)
        {

            Animal _animal = new Animal
            {
                Name = animal.Name,
                Age = animal.Age,
                Species = animal.Species,
                Breed = animal.Breed,
                Photo = animal.Photo,
                Description = animal.Description,
                Weight = animal.Weight,
                Shelter = _context.Shelters.Find(animal.ShelterId)
            };
           
            _context.Animals.Add(_animal);

            await _context.SaveChangesAsync();
           
            animal.Id = _animal.Id;
            return new JsonResult(animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}
