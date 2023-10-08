using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LayoutandForm.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Provide FUll Name")]
        [MaxLength (50)]
        [MinLength (3)]
        [RegularExpression(@"^[a-zA-Z ]{3,50}$", ErrorMessage = "Name  contain only letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your User ID")]
        [MaxLength(12)]
        [MinLength(4)]
        [RegularExpression("^[a-zA-Z0-9_-]{4,12}$", ErrorMessage = "User ID must be 4 to 12 characters long and contain only letters, digits, underscores, or dashes.")]



        public string UserID { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        [MinLength(8)]
        [RegularExpression("^(?=[^A-Z]*[A-Z])(?=(?:[^a-z]*[a-z]){2})(?=(?:[A-Za-z]*[^A-Za-z]){4})[A-Za-z\\d!@#$%^&*()-_+=<>?/\\\\]*$",
        ErrorMessage = "Password does not meet the required criteria.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Enetr your ID in AIUB Formate (xx-xxxxx-x)")]
        [RegularExpression("^[a-zA-Z0-9]{2}-[a-zA-Z0-9]{5}-[a-zA-Z0-9]{1}$", ErrorMessage = "ID must be in the format xx-xxxxx-x.")]
        public string ID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]{2}-[a-zA-Z0-9]{5}-[a-zA-Z0-9]{1}@student.aiub.edu$", ErrorMessage = "Email must follow the ID format and end with @student.aiub.edu.")]
        public string Email
        {
            get { return $"{ID}@student.aiub.edu"; }
            set { ID = value?.Split('@')[0]; }
        }
        [Required(ErrorMessage = "Enter your Date of Birth")]
        public string dob { get; set; }
    }
}