using Task1.Models;
using Task1.Repositories;

namespace Task1.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
    ILogger<ProductService> logger,
    IConfiguration configuration,
    IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _configuration = configuration;
        _logger = logger;
    }
    public async ValueTask<Result<Product>> CreateAsync(Product model, string userId)
    {
        try
        {
            if(model is null)
              return new("Model is null");

            var vat = double.Parse(_configuration.GetValue("VAT",""));
            
            var calculate = new CalculateService();
            var price = calculate.Calculate(vat, model.Quantity, model.Price);

            model.Price = price;
            
           var createdProduct = await _productRepository.AddAsync(model);
            _productRepository.Save(userId);
           
           
           if(createdProduct is null)
              return new("Product not created");
          
           return new(true) {Data = createdProduct};

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
            .Select(x => x)
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

    public async ValueTask<Result<Product>> Remove(int id,string userId)
    {
        try
        {
            var product = _productRepository.GetById(id);

            if(product is null)
              return new("this product is not exist");

            var removedProduct = await _productRepository.Remove(product);

            _productRepository.Save(userId);

            return new(true) {Data = removedProduct};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Product is not removed {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<Product>> Update(int id, Product model,string userId)
    {
        try
        {
            var product = _productRepository.GetById(id);
            
            if(product is null)
              return new("Bu Product mavjud emas");
             
             var vat = double.Parse(_configuration.GetValue("VAT",""));
            
            var calculate = new CalculateService();
            var price = calculate.Calculate(vat, model.Quantity, model.Price);
            
            product.Title = model.Title;
            product.Quantity = model.Quantity;
            product.Price = price;

            var updatedProduct = await _productRepository.Update(product);

            _productRepository.Save(userId);
                
           return new(true) {Data = updatedProduct};

        }
        catch(System.Exception e )
        {
           _logger.LogInformation($"Product not updated {model}");
           throw new Exception(e.Message);
        }
    }
}
