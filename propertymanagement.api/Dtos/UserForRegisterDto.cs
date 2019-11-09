using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Hasło musi składć się z minimum 4, oraz maksimum 20 znaków")]
        public string Password { get; set; }
    }
}