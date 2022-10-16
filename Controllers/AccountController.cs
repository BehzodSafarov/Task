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
    [HttpGet]
    public IActionResult LogIn(string returnUrl) => View(new User() {ReturnUrl = returnUrl});

    [HttpPost]
    public async Task<IActionResult> LogIn(User model)
    {
        if(!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);

        if(user == null) return View(model);

        var result = await _signInMagaer.PasswordSignInAsync(user, model.Password, false, false);

        if(!result.Succeeded)
        {
            return View(model);
        }

        _logger.LogInformation($"Login succsesefull Passed");
       
        var role = await _signInMagaer.UserManager.GetRolesAsync(user); 
        
        if(role[0] == "admin")
         return LocalRedirect($"/Product/List");
        if(role[0] == "user")
          return LocalRedirect($"/User/List");
      
       return LocalRedirect($"{model.ReturnUrl ?? "/"}");
    }

}