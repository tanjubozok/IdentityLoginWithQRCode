using Microsoft.AspNetCore.Mvc;

namespace IdentityLoginWithQRCode.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
