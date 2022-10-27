using Task1.Models;
using Task1.Repositories;

namespace Task1.Services;

public class HistoryService : IHistoryService
{
    private IHistoryRepository _historyRepository;
    private ILogger<HistoryService> _logger;

    public HistoryService(
    ILogger<HistoryService> logger,
    IHistoryRepository historyRepository)
    {
     _historyRepository = historyRepository;
     _logger = logger;
    }
   public async ValueTask<Result<History>> CreateAsync(Models.History entity)
    {
        try
        {
            var createdArxive = await _historyRepository.AddAsync(entity);

            return new(true){Data = createdArxive};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"this arxive not created {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<List<History>>> GetAll()
    {
        try
        {
            var arxives = _historyRepository.GetAll()
            .Select(x => x)
            .ToList();


            return new(true) {Data = arxives};
        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Arxives not taked {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<History>> Remove(int id)
    {
        try
        {
           var arxive = _historyRepository.GetById(id);  

           if(arxive is null)
                return new("this arxive not found");
         
            arxive.RemovedAt = DateTime.UtcNow;
            var removedArxive = await _historyRepository.Remove(arxive);

            return new(true) {Data = removedArxive};
         
        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Arxive not removed {e.Message}");
            throw new Exception(e.Message);
        }
    }
}