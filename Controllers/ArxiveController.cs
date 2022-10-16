using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Services;

namespace Task1.Controllers;

[Authorize(Roles = "admin")]
public class ArxiveController : Controller
{
    private readonly ILogger<ArxiveController> _logger;
    private readonly IArxiveService _service;

    public ArxiveController(
      ILogger<ArxiveController> logger,
      IArxiveService service)
    {
      _logger = logger;
      _service = service;  
    }

    public async Task<IActionResult> List()
    {
       var products = await _service.GetAll();
        
       return View(products.Data);
    }

    public async Task<IActionResult> Remove(int id)
    {
        await _service.Remove(id);

        return RedirectToAction(nameof(List));
    }
}