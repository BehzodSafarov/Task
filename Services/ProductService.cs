using Microsoft.AspNetCore.Authorization;
using Task1.Models;
using Task1.Repositories;

namespace Task1.Services;

[Authorize(Roles = "admin")]
public class ProductService : IProductService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProductService> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IArxiveService _arxiveService;

    public ProductService(
        IConfiguration configuration,
        ILogger<ProductService> logger,
        IProductRepository productRepository,
        IArxiveService arxiveService)
    {
        _configuration = configuration;
        _logger = logger;
        _productRepository = productRepository;
        _arxiveService = arxiveService;
    }


    public async ValueTask<Result<Product>> CreateAsync(Product model)
    {
        try
        {
          if(model is null)
            return new("Model is null");

            var vat = double.Parse(_configuration.GetValue("VAT",""));
            
            var calculate = new CalculateService();
            var price = calculate.Calculate(vat, model.Quantity, model.Price);

           

            model.Price = price;
            
           var createdProduct = await _productRepository
           .AddAsync(model.ToEntity());

           if(createdProduct is null)
              return new("Product not created");
           
           var arxive = model.ToArxiveEntity();
           
           
           arxive.CreatedAt = DateTime.UtcNow;
           await _arxiveService.CreateAsync(arxive);
          
           return new(true) {Data = createdProduct.ToModel()};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Product not created {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<List<Product>>> GetAll()
    {
        try
        {
            var products = _productRepository.GetAll()
            .Select(x => x.ToModel())
            .ToList();

            if(products is null)
              return new("Products not exist");
            
            return new(true) {Data = products};
        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Products not taked {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<Product>> Remove(int id)
    {
        try
        {
            var product = _productRepository.GetById(id);

            if(product is null)
              return new("this product is not exist");

            var removedProduct = await _productRepository.Remove(product);

            var arxive = product.ToModel().ToArxiveEntity();

            arxive.RemovedAt = DateTime.UtcNow;
            await _arxiveService.CreateAsync(arxive);

            return new(true) {Data = removedProduct.ToModel()};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Product is not removed {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<Product>> Update(int id, Product model)
    {
        try
        {
            var product = _productRepository.GetById(id);
            
            if(product is null)
              return new("Bu Product mavjud emas");

            var arxive = product.ToModel().ToArxiveEntity();

            arxive.UpdatedAt = DateTime.UtcNow;
            await _arxiveService.CreateAsync(arxive);
             
             var vat = double.Parse(_configuration.GetValue("VAT",""));
            
            var calculate = new CalculateService();
            var price = calculate.Calculate(vat, model.Quantity, model.Price);
            
            product.Title = model.Title;
            product.Quantity = model.Quantity;
            product.Price = price;

            var updatedProduct = await _productRepository.Update(product);
                

           return new(true) {Data = updatedProduct.ToModel()};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Product not created {e.Message}");
            throw new Exception(e.Message);
        }
    }
}