using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Age { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public List<Favorite> Favorites { get; set; }

        public Adoption Adoption { get; set; }

    }
}
