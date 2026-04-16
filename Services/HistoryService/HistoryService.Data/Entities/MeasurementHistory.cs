namespace HistoryService.Data.Entities;

public class MeasurementHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}