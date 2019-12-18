using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "The password must be between 4 and 20 characters")]
        public string Password { get; set; }
    }
}