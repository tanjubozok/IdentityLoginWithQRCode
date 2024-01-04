namespace IdentityLoginWithQRCode.Controllers;

[Authorize]
public class TwoAuthenticationController(AuthenticatorService authenticatorService, UserManager<AppUser> userManager) : Controller
{
    private readonly AuthenticatorService _authenticatorService = authenticatorService;
    private readonly UserManager<AppUser> _userManager = userManager;

    public IActionResult SelectTwoFactorAuthentication()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SelectTwoFactorAuthentication(TwoFactorTypeSelectViewModel model)
    {
        switch (model.TwoFactorType)
        {
            case TwoFactorType.Authenticator:
                return RedirectToAction("AuthenticatorVerify");
            case TwoFactorType.SMS:
                return RedirectToAction("SMSVerify");
            case TwoFactorType.Email:
                return RedirectToAction("EmailVerify");
            default:
                break;
        }
        return View();
    }

    public async Task<IActionResult> AuthenticatorVerify()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        string sharedKey = await _authenticatorService.GenerateSharedKey(user);
        string qrcodeUri = await _authenticatorService.GenerateQrCodeUri(sharedKey, "www.tanjubozok.com", user);

        return View(new AuthenticatorViewModel
        {
            SharedKey = sharedKey,
            QrCodeUri = qrcodeUri
        });
    }

    [HttpPost]
    public async Task<IActionResult> AuthenticatorVerify(AuthenticatorViewModel model)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        var verifyState = await _authenticatorService.Verify(model, user);
        if (verifyState.State)
        {
            TempData["message2"] = "İki adımlı doğrulama hesaba tanımlanmıştır.";
            TempData["message3"] = verifyState.RecoveryCode;
        }
        return View(model);
    }
}