using Common.Models.Enums;

namespace MeasurementService.Business.Converters;

public static class WeightConverter
{
    public static double ConvertToKilograms(double value, WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Kilogram => value,
            WeightUnit.Gram => value / 1000,
            WeightUnit.Milligram => value / 1000000,
            WeightUnit.Ton => value * 1000,
            WeightUnit.Pound => value * 0.453592,
            WeightUnit.Ounce => value * 0.0283495,
            _ => throw new ArgumentException("Invalid weight unit")
        };
    }

    public static double ConvertFromKilograms(double kg, WeightUnit targetUnit)
    {
        return targetUnit switch
        {
            WeightUnit.Kilogram => kg,
            WeightUnit.Gram => kg * 1000,
            WeightUnit.Milligram => kg * 1000000,
            WeightUnit.Ton => kg / 1000,
            WeightUnit.Pound => kg / 0.453592,
            WeightUnit.Ounce => kg / 0.0283495,
            _ => throw new ArgumentException("Invalid weight unit")
        };
    }
}