using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Save_A_Soul.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public float Salary { get; set; }
        public string JobType { get; set; }
        public string Position  { get; set; }

        public virtual Shelter Shelter { get; set; }
    }
}
