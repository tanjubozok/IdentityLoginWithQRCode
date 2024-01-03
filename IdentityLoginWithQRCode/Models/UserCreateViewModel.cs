using System.ComponentModel.DataAnnotations;

namespace IdentityLoginWithQRCode.Models;

public class UserCreateViewModel
{
    [Required(ErrorMessage = "Lütfen adı boş geçmeyiniz.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
    public string Password { get; set; } = string.Empty;
}
