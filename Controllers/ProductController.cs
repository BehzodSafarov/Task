using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    private readonly IHistoryService _historyService;

    public ProductController(
        ILogger<ProductController> logger,
        IProductService service,
        IHistoryService historyService)
    {
        _logger = logger;
        _productService = service;
        _historyService = historyService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> List()
    {
      var products = await _productService.GetAll();
        
       return View(products.Data);
    }
    public IActionResult AddProduct() => View();
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddProduct(Product model)
    {
         int number = 0; 
         var price = model.Price.ToString();
         
          if(model.Price <= 0 )
         {
            ViewBag.Message = false;

            return View();
         }
         else if(model.Quantity <= 0 )
         {
            ViewBag.mana = false;

            return View();
         }
          else if(model.Title is null)
         {
            ViewBag.Null = false;

            return View();
         }
         else if(!int.TryParse(price,out number))
         {
            ViewBag.Xato = false;
            
            return View();
         }

        await _productService.CreateAsync(model);

        return RedirectToAction(nameof(List));
    } 
     
     public IActionResult Update() => View();

     [HttpPost]
     [Authorize(Roles ="admin")]
    public async Task<IActionResult> Update(int id, Product model)
    {

     int number = 0; 
         var price = model.Price.ToString();
         
          if(model.Price <= 0 )
         {
            ViewBag.PriceNullOrMinus = false;

            return View();
         }
         else if(model.Quantity <= 0 )
         {
            ViewBag.QuontityNullOrMinus = false;

            return View();
         }
          else if(model.Title is null)
         {
            ViewBag.TitleNull = false;

            return View();
         }
         else if(!int.TryParse(price,out number))
         {
            ViewBag.PriceString = false;
            
            return View();
         }

       await _productService.Update(id, model);

       return RedirectToAction(nameof(List));
    }
    [Authorize(Roles = "admin")]
    public IActionResult Remove(int id)
    {
        
        var product = _productService.Remove(id);

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> PublicList()
    {
      var products = await _productService.GetAll();
        
      return View(products.Data);
    }

     [Authorize(Roles = "admin")]
    public async Task<IActionResult> HistoryList()
    {
       var products = await _historyService.GetAll();
        
       return View(products.Data);
    }
    
}