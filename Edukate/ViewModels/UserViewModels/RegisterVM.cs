using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(255), MinLength(2)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(2), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(2), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(255), MinLength(2), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
