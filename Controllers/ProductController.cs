using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Repositories;
using Task1.Services;

namespace Task1.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IProductService _productService;

    public ProductController(
        ILogger<ProductController> logger,
        IProductService service,
        AppDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
        _productService = service;
    }

    [HttpGet]
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

      var user1 = User.Identity!.Name;

      var user = _userManager.Users.FirstOrDefault(x => x.UserName == user1);
      

      await _productService.CreateAsync(model, user!.Id.ToString());

      
      
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
         else if(model.Quantity <= 0)
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

         var user1 = User.Identity!.Name;

         var user = _userManager.Users.FirstOrDefault(x => x.UserName == user1);
   

       await _productService.Update(id, model,user!.Id.ToString());

       return RedirectToAction(nameof(List));
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Remove(int id)
    {

        var user1 = User.Identity!.Name;

         var user = _userManager.Users.FirstOrDefault(x => x.UserName == user1);
   
        var product = _productService.Remove(id,user!.Id.ToString());

        return RedirectToAction(nameof(List));
    }


    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult HistoryList()
    {
      var histories = _context.AuditLogs!.ToList();

      if(histories is null)
      return View();
      
      return View(histories);
    }
}

    
