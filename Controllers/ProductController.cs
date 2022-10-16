using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers;

[Authorize(Roles = "admin")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;

    public ProductController(
        ILogger<ProductController> logger,
        IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
      var products = await _service.GetAll();
        
       return View(products.Data);
    }
    public IActionResult AddProduct() => View();
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product model)
    {
        await _service.CreateAsync(model);

        return RedirectToAction(nameof(List));
    } 
     
     public IActionResult Update() => View();

     [HttpPost]
    public async Task<IActionResult> Update(int id, Product model)
    {
       await _service.Update(id, model);

       return RedirectToAction(nameof(List));
    }
    public IActionResult Remove(int id)
    {
        var product = _service.Remove(id);

        return RedirectToAction(nameof(List));
    }

}