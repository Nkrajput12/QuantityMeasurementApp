using Common.Models.Enums;

namespace MeasurementService.Business.Converters;

public static class VolumeConverter
{
    public static double ConvertToLiters(double value, VolumeUnit unit)
    {
        return unit switch
        {
            VolumeUnit.Liter => value,
            VolumeUnit.Milliliter => value / 1000,
            VolumeUnit.Gallon => value * 3.78541,
            VolumeUnit.Quart => value * 0.946353,
            _ => throw new ArgumentException("Invalid volume unit")
        };
    }

    public static double ConvertFromLiters(double liters, VolumeUnit targetUnit)
    {
        return targetUnit switch
        {
            VolumeUnit.Liter => liters,
            VolumeUnit.Milliliter => liters * 1000,
            VolumeUnit.Gallon => liters / 3.78541,
            VolumeUnit.Quart => liters / 0.946353,
            _ => throw new ArgumentException("Invalid volume unit")
        };
    }
}