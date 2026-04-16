using Common.Models.Enums;

namespace MeasurementService.Business.Converters;

public static class LengthConverter
{
    public static double ConvertToMeters(double value, LengthUnit unit)
    {
        return unit switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.34,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            LengthUnit.Inch => value * 0.0254,
            _ => throw new ArgumentException("Invalid length unit")
        };
    }

    public static double ConvertFromMeters(double meters, LengthUnit targetUnit)
    {
        return targetUnit switch
        {
            LengthUnit.Meter => meters,
            LengthUnit.Kilometer => meters / 1000,
            LengthUnit.Centimeter => meters * 100,
            LengthUnit.Millimeter => meters * 1000,
            LengthUnit.Mile => meters / 1609.34,
            LengthUnit.Yard => meters / 0.9144,
            LengthUnit.Foot => meters / 0.3048,
            LengthUnit.Inch => meters / 0.0254,
            _ => throw new ArgumentException("Invalid length unit")
        };
    }
}