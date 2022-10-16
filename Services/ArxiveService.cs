using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.Models;
using Task1.Repositories;

namespace Task1.Services;

public class ArxiveService : IArxiveService
{
   
    private readonly ILogger<ArxiveService> _logger;
    private readonly IArxiveRepository _arxiveRepository;

    public ArxiveService(
        ILogger<ArxiveService> logger,
        IArxiveRepository arxiveRepository)
    {
        _logger = logger;
        _arxiveRepository = arxiveRepository;
    }

    
    public async ValueTask<Result<Arxive>> CreateAsync(Arxive model)
    {
        try
        {
            var createdArxive = await _arxiveRepository.AddAsync(model.ToEntity());

            return new(true){Data = createdArxive.ToModel()};

        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"this arxive not created {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<List<Arxive>>> GetAll()
    {
        try
        {
            var arxives = _arxiveRepository.GetAll()
            .Select(x => x.ToModel())
            .ToList();


            return new(true) {Data = arxives};
        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Arxives not taked {e.Message}");
            throw new Exception(e.Message);
        }
    }

    public async ValueTask<Result<Arxive>> Remove(int id)
    {
        try
        {
           var arxive = _arxiveRepository.GetById(id);  

           if(arxive is null)
                return new("this arxive not found");
         
            arxive.RemovedAt = DateTime.UtcNow;
            var removedArxive = await _arxiveRepository.Remove(arxive);

            return new(true) {Data = removedArxive.ToModel()};
         
        }
        catch (System.Exception e)
        {
            _logger.LogInformation($"Arxive not removed {e.Message}");
            throw new Exception(e.Message);
        }
    }

}