using Task1.Models;

namespace Task1.Services;

public interface IProductService
{
    ValueTask<Result<Product>> CreateAsync(Product model,string userId);
    ValueTask<Result<Product>> Update(int id, Product model,string userId);
    ValueTask<Result<Product>> Remove(int id, string userId);
    ValueTask<Result<List<Product>>> GetAll();
}