using Task1.Data;
using Task1.Entities;

namespace Task1.Repositories;

public class ProductRepository : GenericRepository<Product> , IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context){}
}