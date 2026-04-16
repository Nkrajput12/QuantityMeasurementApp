using Common.Models.DTOs;

namespace Common.Models.DTOs;

public class MeasurementApiRequest
{
    public string Category { get; set; } = string.Empty;
    public QuantityDto Value1 { get; set; } = new();
    public QuantityDto? Value2 { get; set; }
    public string? TargetUnit { get; set; }
}