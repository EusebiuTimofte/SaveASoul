﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Description { get; set; }
        public string BankAccout { get; set; }
        public List<Employee> Employees { get; set; }//1-M
        public List<Animal> AvailableAnimals { get; set; }//1-M

    }
}
