using Common.Models.Enums;

namespace MeasurementService.Business.Converters;

public static class TemperatureConverter
{
    public static double Convert(double value, TemperatureUnit from, TemperatureUnit to)
    {
        if (from == to) return value;

        // Convert to Celsius first
        double celsius = from switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
            TemperatureUnit.Kelvin => value - 273.15,
            _ => throw new ArgumentException("Invalid temperature unit")
        };

        // Convert from Celsius to target
        return to switch
        {
            TemperatureUnit.Celsius => celsius,
            TemperatureUnit.Fahrenheit => celsius * 9 / 5 + 32,
            TemperatureUnit.Kelvin => celsius + 273.15,
            _ => throw new ArgumentException("Invalid temperature unit")
        };
    }
}