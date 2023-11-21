using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EmpStatus { get; set; }


        public virtual ICollection<Process> Processes { get; set; }

        public Employee()
        {
            Processes = new List<Process>();
        }


    }
}