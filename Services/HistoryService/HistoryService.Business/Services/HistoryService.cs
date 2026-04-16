using System.Text.Json;
using HistoryService.Business.Interfaces;
using HistoryService.Data.Entities;
using HistoryService.Data.Interfaces;

namespace HistoryService.Business.Services;

public class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _historyRepository;

    public HistoryService(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public void SaveHistory(int userId, string operation, string category, object details)
    {
        var history = new MeasurementHistory
        {
            UserId = userId,
            Operation = operation,
            Category = category,
            Details = JsonSerializer.Serialize(details),
            Timestamp = DateTime.UtcNow
        };

        _historyRepository.AddHistory(history);
    }

    public List<MeasurementHistory> GetHistoryByUserId(int userId)
    {
        return _historyRepository.GetHistoryByUserId(userId);
    }

    public List<MeasurementHistory> GetHistoryByOperation(string operation, int userId)
    {
        return _historyRepository.GetHistoryByOperation(operation, userId);
    }

    public int GetOperationCount(int userId)
    {
        return _historyRepository.GetOperationCount(userId);
    }
}