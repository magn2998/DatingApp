using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class ProfileForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 16 characters")]
        public string Password { get; set; }

        [Required]
        public string ProfileName { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ProfileForRegisterDto() 
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}