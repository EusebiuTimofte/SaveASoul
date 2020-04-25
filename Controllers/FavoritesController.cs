using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Contexts;
using Save_A_Soul.DTOs;
using Save_A_Soul.Models;

namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly Context _context;

        public FavoritesController(Context context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<JsonResult> GetFavorites()
        {
            List<Favorite>favorites = await _context.Favorites.ToListAsync();

            List<FavoriteDTO> favoriteDTOs = new List<FavoriteDTO>();

            foreach(Favorite favorite in favorites)
            {
                FavoriteDTO tempFav = new FavoriteDTO
                {
                    UserId = favorite.UserId,
                    AnimalId = favorite.AnimalId
                };
                favoriteDTOs.Add(tempFav);
            }

            return new JsonResult(favoriteDTOs);
        }

        // GET: api/Favorites/ofUser/5
        [Route("ofUser/{id}")]
        [HttpGet] 
        public async Task<JsonResult> GetFavoritesAnimalsOfUser(int id)
        {
            var favorites = await _context.Favorites.ToListAsync();
            if (favorites == null)
            {
                return new JsonResult("Nu a putut lua din BD inregistrarile din tabelul Favorites");
            }
            List<FavoriteDTO> userFavorites = new List<FavoriteDTO>();

            foreach(Favorite favorite in favorites)
            {
                if (favorite.UserId == id)
                {
                    FavoriteDTO userFavorite = new FavoriteDTO
                    {
                        UserId = favorite.UserId,
                        AnimalId = favorite.AnimalId
                    };
                    userFavorites.Add(userFavorite);
                } 
            }

            return new JsonResult(userFavorites);
        }


        // GET: api/Favorites/ofAnimal/5
        [HttpGet]
        [Route("ofAnimal/{id}")]
        public async Task<JsonResult> GetFavoritesUsersOfanimal(int id)
        {
            var favorites = await _context.Favorites.ToListAsync();
            if (favorites == null)
            {
                return new JsonResult("Nu a putut lua din BD inregistrarile din tabelul Favorites");
            }
            List<FavoriteDTO> animalFavorites = new List<FavoriteDTO>();

            foreach (Favorite favorite in favorites)
            {
                if (favorite.AnimalId == id)
                {
                    FavoriteDTO userFavorite = new FavoriteDTO
                    {
                        UserId = favorite.UserId,
                        AnimalId = favorite.AnimalId
                    };
                    animalFavorites.Add(userFavorite);
                }
            }

            return new JsonResult(animalFavorites);
        }


        /*// PUT: api/Favorites/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<JsonResult> PutFavorite(int id, FavoriteDTO favorite)
        {
            if (id != favorite.AnimalId)
            {
                return BadRequest();
            }

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Favorites
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<JsonResult> PostFavorite(FavoriteDTO favorite)
        {

            if (favorite.AnimalId <= 0 || favorite.UserId <= 0)
            {
                return new JsonResult("ids are mandatory and they must be positive");
            }

            Favorite favoriteForDB = new Favorite
            {
                UserId = favorite.UserId,
                AnimalId = favorite.AnimalId,
                User = _context.Users.Find(favorite.UserId),
                Animal = _context.Animals.Find(favorite.AnimalId)
            };

            _context.Favorites.Add(favoriteForDB);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavoriteExists(favorite.UserId, favorite.AnimalId))
                {
                    return new JsonResult("This favorite db entry already exists");
                }
                else
                {
                    throw;
                }
            }
            
            return new JsonResult(favorite);
        }

        // DELETE: api/Favorites/5/2
        [HttpDelete("{userId}/{animalId}")]
        public async Task<JsonResult> DeleteFavorite(int userId, int animalId)
        {
            var favorite = await _context.Favorites.FindAsync(userId, animalId);
            if (favorite == null)
            {
                return new JsonResult("Ce vrai tu sa stergi nu se exista");
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            FavoriteDTO favoriteReturn = new FavoriteDTO
            {
                UserId = favorite.UserId,
                AnimalId = favorite.AnimalId
            };

            return new JsonResult(favoriteReturn);
        }

        // DELETE: api/Favorites/ofUser/2
        [HttpDelete]
        [Route("ofUser/{id}")]
        public async Task<JsonResult> DeleteFavoritesOfUser(int id)
        {
            var favorites = await _context.Favorites.ToListAsync();
            if (favorites == null)
            {
                return new JsonResult("Ce vrai tu sa stergi nu se exista");
            }

            List<FavoriteDTO> favoritesReturn = new List<FavoriteDTO>();

            foreach(Favorite favorite in favorites)
            {
                if (favorite.UserId == id)
                {
                    _context.Favorites.Remove(favorite);
                    await _context.SaveChangesAsync();
                    FavoriteDTO favoriteReturn = new FavoriteDTO
                    {
                        UserId = favorite.UserId,
                        AnimalId = favorite.AnimalId
                    };
                    favoritesReturn.Add(favoriteReturn);
                }
            }     

            return new JsonResult(favoritesReturn);
        }

        // DELETE: api/Favorites/ofAnimal/2
        [HttpDelete]
        [Route("ofAnimal/{id}")]
        public async Task<JsonResult> DeleteFavoritesOfanimal(int id)
        {
            var favorites = await _context.Favorites.ToListAsync();
            if (favorites == null)
            {
                return new JsonResult("Ce vrai tu sa stergi nu se exista");
            }

            List<FavoriteDTO> favoritesReturn = new List<FavoriteDTO>();

            foreach (Favorite favorite in favorites)
            {
                if (favorite.AnimalId == id)
                {
                    _context.Favorites.Remove(favorite);
                    await _context.SaveChangesAsync();
                    FavoriteDTO favoriteReturn = new FavoriteDTO
                    {
                        UserId = favorite.UserId,
                        AnimalId = favorite.AnimalId
                    };
                    favoritesReturn.Add(favoriteReturn);
                }
            }

            return new JsonResult(favoritesReturn);
        }


        private bool FavoriteExists(int userId, int animalId)
        {
            return _context.Favorites.Any(e => e.AnimalId == animalId && e.UserId == userId);
        }
    }
}
