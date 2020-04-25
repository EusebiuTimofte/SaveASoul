using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class Favorite
    {
        //public int Id { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
