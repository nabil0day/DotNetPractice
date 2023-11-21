using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class Process
    {
        public int Id { get; set; }

        [ForeignKey("Request")]
        public int Rid { get; set; }

        [ForeignKey("Employee")]
        public int Eid { get; set; }

        public string Foodname { get; set; }

        public int Quantity { get; set; }

        public string OrderStatus { get; set; }

        public string EmpStatus { get; set; }

        public virtual Request Request { get; set; }

        public virtual Employee Employee { get; set; }
    }
}