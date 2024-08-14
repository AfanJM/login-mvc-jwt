using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace login_mvc_jwt.Dto.Users
{
    public class registerDto
    {
        [Required(ErrorMessage ="The userName is required")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string email { get; set; } = null!;

        [Required]
        [PasswordPropertyText]
        public string password { get; set; } = null!;

    }
}
