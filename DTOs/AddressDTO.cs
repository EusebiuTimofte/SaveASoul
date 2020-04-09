using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }

    }
}
