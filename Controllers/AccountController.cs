using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;

namespace Task1.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInMagaer;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInMagaer = signInManager;
    }
    
    public IActionResult LogIn(string returnUrl) => View();
    public IActionResult Register(string returnUrl) => View();

    [HttpPost]
    public async Task<IActionResult> LogIn(User model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null)
        {
            ViewBag.NotSignedId = false;
            return View(model);
        }

        var result = await _signInMagaer.PasswordSignInAsync(user, model.PasswordHash, false, false);

        if (!result.Succeeded)
        {
            ViewBag.NotSignedId = false;
            return View(model);
        }

        _logger.LogInformation($"Login succsesefull Passed");

        var role = await _signInMagaer.UserManager.GetRolesAsync(user);

        if (role[0] == "admin")
            return LocalRedirect($"/Product/List");

        if (role[0] == "user")
            return LocalRedirect($"/Product/PublicList");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {
        
        if(model.UserName is null)
          {
            ViewBag.NullName = true;
            return View();
          }
          if(model.PasswordHash is null || model.PasswordHash.Count() < 6)
          {
           ViewBag.PasswordNull = true;
           return View();
          }
        
        var user = new IdentityUser(model.UserName);

        var result = await _userManager.CreateAsync(user, model.PasswordHash);

        await _userManager.AddToRoleAsync(user,"user");
        if(!result.Succeeded) return View(model);


        return RedirectToAction(nameof(LogIn));
    }

}