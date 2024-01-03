using IdentityLoginWithQRCode.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace IdentityLoginWithQRCode.Services;

public class AuthenticatorService(UserManager<AppUser> userManager, UrlEncoder urlEncoder)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly UrlEncoder _urlEncoder = urlEncoder;

    public async Task<string> GenerateSharedKey(AppUser user)
    {
        string sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(sharedKey))
        {
            var result = await _userManager.ResetAuthenticatorKeyAsync(user);
            if (result.Succeeded)
                sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }
        return sharedKey;
    }

    public async Task<string> GenerateQrCodeUri(string sharedKey, string title, AppUser user)
    {
        return $"otpauth://totp/{_urlEncoder.Encode(title)}:{_urlEncoder.Encode(user.Email)}?secret={sharedKey}&issuer={_urlEncoder.Encode(title)}";
    }

    public async Task<VerifyState> Verify(AuthenticatorViewModel model, AppUser user)
    {
        VerifyState verifyState = new()
        {
            State = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.VerificationCode)
        };

        if (verifyState.State)
        {
            user.TwoFactorEnabled = true;
            verifyState.RecoveryCode = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
        }
        return verifyState;
    }
}
