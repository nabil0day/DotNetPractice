using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }

        public DateTime MaxTime { get; set; }

        public string Foodname { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public Restaurant()
        {
            Requests = new List<Request>();
        }


    }
}