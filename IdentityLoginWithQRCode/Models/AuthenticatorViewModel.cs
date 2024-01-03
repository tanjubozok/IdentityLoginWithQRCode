namespace IdentityLoginWithQRCode.Models;

public class AuthenticatorViewModel
{
    public string SharedKey { get; set; } = string.Empty;
    public string QrCodeUri { get; set; } = string.Empty;
    public string VerificationCode { get; set; } = string.Empty;
}
