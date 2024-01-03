using System.ComponentModel.DataAnnotations;

namespace IdentityLoginWithQRCode.Models;

public class UserSignViewModel
{
    [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz.")]
    [StringLength(55, MinimumLength = 10, ErrorMessage = "Lütfen email adresini 10 - 55 karakter aralığında giriniz.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
    public string Password { get; set; } = string.Empty;
}
