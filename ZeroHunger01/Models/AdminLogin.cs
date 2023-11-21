using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger01.Models
{
    public class AdminLogin
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}