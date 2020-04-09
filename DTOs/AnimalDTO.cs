using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Save_A_Soul.Models;

namespace Save_A_Soul.DTOs
{
    public class AnimalDTO
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

    }
}
