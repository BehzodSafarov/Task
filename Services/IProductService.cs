using Task1.Models;

namespace Task1.Services;

public interface IProductService
{
    ValueTask<Result<Product>> CreateAsync(Product model);
    ValueTask<Result<Product>> Update(int id, Product model);
    ValueTask<Result<Product>> Remove(int id);
    ValueTask<Result<List<Product>>> GetAll();
 }