using Task1.Data;
using Task1.Entities;

namespace Task1.Repositories;

public class ArxiveRepository : GenericRepository<Arxive>, IArxiveRepository
{
    public ArxiveRepository(AppDbContext context) : base(context)
    {
    }
}