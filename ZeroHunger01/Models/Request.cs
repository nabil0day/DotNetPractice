using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class Request
    {
        public int Id { get; set; }
        [ForeignKey("Restaurant")]
        public int rid { get; set; }

        public string Location { get; set; }

        public DateTime MaxTime { get; set; }

        public string Foodname { get; set; }

        public int Quantity { get; set; }

        public string OrderStatus { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Process> Processes { get; set; }

        public Request()
        {
            Processes = new List<Process>();
        }
    }
}