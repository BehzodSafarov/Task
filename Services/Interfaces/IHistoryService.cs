using Task1.Models;

namespace Task1.Services;

public interface IHistoryService
{
    ValueTask<Result<History>> CreateAsync(Models.History entity);
    ValueTask<Result<History>> Remove(int id);
    ValueTask<Result<List<History>>> GetAll();
}