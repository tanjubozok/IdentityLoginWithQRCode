using System.ComponentModel.DataAnnotations;

namespace IdentityLoginWithQRCode.Models;

public enum TwoFactorType
{
    [Display(Name = "Microsoft & Google Authenticator İle Doğrulama")]
    Authenticator,
    [Display(Name = "SMS İle Doğrulama")]
    SMS,
    [Display(Name = "E-Posta İle Doğrulama")]
    Email
}
