namespace IdentityLoginWithQRCode.Models;

public class VerifyState
{
    public bool State { get; set; }
    public IEnumerable<string>? RecoveryCode { get; set; }
}