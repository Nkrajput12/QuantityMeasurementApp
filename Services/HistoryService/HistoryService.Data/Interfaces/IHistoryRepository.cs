using HistoryService.Data.Entities;

namespace HistoryService.Data.Interfaces;

public interface IHistoryRepository
{
    void AddHistory(MeasurementHistory history);
    List<MeasurementHistory> GetHistoryByUserId(int userId);
    List<MeasurementHistory> GetHistoryByOperation(string operation, int userId);
    int GetOperationCount(int userId);
}