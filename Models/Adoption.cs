﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class Adoption
    {
        //public int Id { get; set; }
        public DateTime AdoptionTime { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
