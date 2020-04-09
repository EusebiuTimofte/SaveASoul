using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Save_A_Soul.Models;

namespace Save_A_Soul.DTOs
{
    public class AdoptionDTO
    {
        public DateTime AdoptionTime { get; set; }
        public int AnimalId { get; set; }
        public int UserId { get; set; }
    }
}
