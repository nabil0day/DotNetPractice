using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class ZHContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Process> Processes { get; set; }

    }
}