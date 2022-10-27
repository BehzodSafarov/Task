
using Task1.Models;

namespace Task1.Repositories;

public class HistoryRepository : GenericRepository<History>, IHistoryRepository
{
    public HistoryRepository(AppDbContext context) : base(context)
    {
    }
}