using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual List<Adoption> Adoption { get; set; }

        public virtual List<Favorite> Favorites { get; set; }
    }
}
