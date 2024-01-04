namespace IdentityLoginWithQRCode.Controllers;

public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public IActionResult Login(string ReturnUrl = "")
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserSignViewModel model, string ReturnUrl = "")
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(ReturnUrl);
                }
                else if (result.RequiresTwoFactor)
                    return RedirectToAction("TwoFactorAuthenticate", new { ReturnUrl = ReturnUrl });
            }
        }
        return View(model);
    }

    public IActionResult TwoFactorAuthenticate(string ReturnUrl = "")
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> TwoFactorAuthenticate(TwoFactorLoginViewModel model, string ReturnUrl = "")
    {
        Microsoft.AspNetCore.Identity.SignInResult result = null;

        if (model.Recovery)
            result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(model.VerifyCode);
        else
            result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.VerifyCode, true, false);

        if (result.Succeeded)
            return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/home/index" : ReturnUrl);
        else
            ModelState.AddModelError("verifycode", "Doğrulama kodu yanlış girilmiştir!");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost()]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                AppUser appUser = new()
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    TempData["message1"] = "Kayıt başarılı. Giriş yapabilirsiniz.";
                    return RedirectToAction("Login", "Account");
                }
            }
        }
        return View(model);
    }
}
