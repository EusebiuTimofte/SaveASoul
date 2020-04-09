using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Save_A_Soul.Models;

namespace Save_A_Soul.DTOs
{
    public class ShelterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Description { get; set; }
        public string BankAccout { get; set; }
    }
}
