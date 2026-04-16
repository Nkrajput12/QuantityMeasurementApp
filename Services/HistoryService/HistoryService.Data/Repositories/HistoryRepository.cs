using HistoryService.Data.Entities;
using HistoryService.Data.Interfaces;

namespace HistoryService.Data.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly HistoryDbContext _context;

    public HistoryRepository(HistoryDbContext context)
    {
        _context = context;
    }

    public void AddHistory(MeasurementHistory history)
    {
        _context.MeasurementHistories.Add(history);
        _context.SaveChanges();
    }

    public List<MeasurementHistory> GetHistoryByUserId(int userId)
    {
        return _context.MeasurementHistories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.Timestamp)
            .ToList();
    }

    public List<MeasurementHistory> GetHistoryByOperation(string operation, int userId)
    {
        return _context.MeasurementHistories
            .Where(h => h.UserId == userId && h.Operation == operation)
            .OrderByDescending(h => h.Timestamp)
            .ToList();
    }

    public int GetOperationCount(int userId)
    {
        return _context.MeasurementHistories
            .Count(h => h.UserId == userId);
    }
}