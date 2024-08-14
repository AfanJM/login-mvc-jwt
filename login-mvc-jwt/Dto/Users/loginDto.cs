using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace login_mvc_jwt.Dto.Users
{
    public class loginDto
    {

        [Required]
        [EmailAddress]
        public string email { get; set; } = null!;

        [Required]
        [PasswordPropertyText]
        public string password { get; set; } = null!;
    }
}
