using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.UserViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(255), MinLength(2), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(2), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
