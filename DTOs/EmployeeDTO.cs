using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Save_A_Soul.Models;

namespace Save_A_Soul.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public float Salary { get; set; }
        public string JobType { get; set; }
        public string Position  { get; set; }
        public int ShelterId { get; set; }
    }
}
