namespace IdentityLoginWithQRCode.Models;

public class TwoFactorLoginViewModel
{
    [Required(ErrorMessage = "Lütfen doğrulama kodunu boş geçmeyiniz.")]
    [Display(Name = "Doğrulama Kodu")]
    public string VerifyCode { get; set; } = string.Empty;

    public bool Recovery { get; set; }
}
