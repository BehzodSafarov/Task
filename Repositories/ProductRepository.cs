
using Task1.Models;

namespace Task1.Repositories;

public class ProductRepository : GenericRepository<Product> , IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context){}
    
}