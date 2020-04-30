using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Contexts;
using Save_A_Soul.Models;
namespace SaveASoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<JsonResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            List<UserDTO> usersReturn = new List<UserDTO>();
            foreach(User user in users)
            {
                UserDTO tempUser = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CNP = user.CNP,
                    Email = user.Email,
                    Password = user.Password
                };

                usersReturn.Add(tempUser);
            }
            return new JsonResult(usersReturn);

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {

                return NotFound("User you're looking for doesn't exist");
            }

            UserDTO returnUser = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.CNP,
                Email = user.Email,
                Password = user.Password
            };

            return new JsonResult(returnUser);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest("id din url trb sa fie acelasi cu id din body");
            }

            User userForDB = _context.Users.Find(id);

            if (userForDB == null)
            {
                return NotFound("User you want to edit doesn't exist");
            }

            userForDB.Id = user.Id;
            userForDB.FirstName = user.FirstName;
            userForDB.LastName = user.LastName;
            userForDB.CNP = user.CNP;
            userForDB.Email = user.Email;
            userForDB.Password = user.Password;

            _context.Entry(userForDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(user);
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO user)
        {

            User userForDB = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.CNP,
                Email = user.Email,
                Password = user.Password
            };

            _context.Users.Add(userForDB);
            await _context.SaveChangesAsync();
            user.Id = userForDB.Id;
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("nu iaste domle useru in bd");
            }

            UserDTO returnUser = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CNP = user.CNP,
                Email = user.Email,
                Password = user.Password
            };

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new JsonResult(returnUser);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
