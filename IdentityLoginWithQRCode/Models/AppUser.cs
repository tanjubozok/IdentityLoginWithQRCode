namespace IdentityLoginWithQRCode.Models;

public class AppUser : IdentityUser<int>
{
    public TwoFactorType TwoFactorType { get; set; }
}
