using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class SignUpModel
    {
        [Required]
        [Range(typeof(bool), "true", "true", 
            ErrorMessage = "The Terms must be accepted.")]
        public bool Accept { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}