using HistoryService.Data.Entities;

namespace HistoryService.Business.Interfaces;

public interface IHistoryService
{
    void SaveHistory(int userId, string operation, string category, object details);
    List<MeasurementHistory> GetHistoryByUserId(int userId);
    List<MeasurementHistory> GetHistoryByOperation(string operation, int userId);
    int GetOperationCount(int userId);
}