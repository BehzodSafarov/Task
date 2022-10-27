using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Services;

namespace Task1.Controllers;

[Authorize(Roles = "admin")]
public class HistoryController : Controller
{
    private readonly ILogger<HistoryController> _logger;
    private readonly IHistoryService _service;

    public HistoryController(
      ILogger<HistoryController> logger,
      IHistoryService service)
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