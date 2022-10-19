using Task1.Models;

namespace Task1.Services;

public interface IArxiveService
{
    ValueTask<Result<Arxive>> CreateAsync(Entities.Arxive entity);
    ValueTask<Result<Arxive>> Remove(int id);
    ValueTask<Result<List<Arxive>>> GetAll();

}