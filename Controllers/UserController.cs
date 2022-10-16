using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Services;

namespace Task1.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IProductService _service;

    public UserController(
        ILogger<UserController> logger,
        IProductService productService)
        {
            _logger = logger;
            _service = productService;
        }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
      var products = await _service.GetAll();
        
       return View(products.Data);
    }
    
}